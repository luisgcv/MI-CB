import { Injectable, NotFoundException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Response } from 'express';
import PDFDocument from 'pdfkit';
import { PurchaseOrderEntity } from '../../database/entities/purchase-order.entity';

@Injectable()
export class PurchaseOrdersService {
    constructor(
        @InjectRepository(PurchaseOrderEntity)
        private orderRepository: Repository<PurchaseOrderEntity>,
    ) { }

    async getOrders(providerId: string) {
        const orders = await this.orderRepository.find({
            where: { providerId },
            relations: ['branch'],
            order: { createdAt: 'DESC' },
        });

        return orders.map((o) => this.mapSummary(o));
    }

    async getOrderDetail(id: string, providerId: string) {
        const order = await this.orderRepository.findOne({
            where: { id, providerId },
            relations: ['branch', 'lines'],
        });

        if (!order) throw new NotFoundException('Orden no encontrada');

        return {
            ...this.mapSummary(order),
            lines: order.lines.map((l) => ({
                sku: l.sku,
                description: l.description,
                cost: Number(l.cost),
                quantityOrdered: Number(l.quantityOrdered),
                quantityReceived: Number(l.quantityReceived),
                quantityReturned: Number(l.quantityReturned),
                quantityBackorder: Number(l.quantityBackorder),
            })),
        };
    }

    private mapSummary(o: PurchaseOrderEntity) {
        return {
            id: o.id,
            branch: o.branch?.nombreSucursal ?? '-',
            statusId: o.statusId,
            status: this.getStatus(o.statusId),
            createdAt: o.createdAt,
            requiredAt: o.requiredAt,
            createdBy: o.createdBy,
            total: Number(o.total),
        };
    }

    private getStatus(statusId: number): string {
        const statuses: Record<number, string> = {
            1: 'Pendiente',
            2: 'Aprobada',
            3: 'Rechazada',
            4: 'En tránsito',
            5: 'Recibida',
        };
        return statuses[statusId] ?? 'Desconocido';
    }

    async getPdfDetails(id: string, providerId: string, res: Response) {
        const order = await this.orderRepository.findOne({
            where: { id, providerId },
            relations: ['branch', 'lines'],
        });

        if (!order) throw new NotFoundException('Orden no encontrada');

        res.setHeader('Content-Type', 'application/pdf');
        res.setHeader(
            'Content-Disposition',
            `attachment; filename="orden-${id}.pdf"`,
        );

        const doc = new PDFDocument({ margin: 50 });
        doc.pipe(res);

        // Encabezado
        doc
            .fontSize(18)
            .font('Helvetica-Bold')
            .text('Detalle de Orden de Compra', { align: 'center' });
        doc.moveDown();

        // Información general
        doc.fontSize(12).font('Helvetica-Bold').text('Informacion general');
        doc.moveTo(50, doc.y).lineTo(550, doc.y).stroke();
        doc.moveDown(0.5);

        const info = [
            ['Consecutivo', order.id],
            ['Sucursal', order.branch?.nombreSucursal ?? '-'],
            ['Creado por', order.createdBy],
            ['Fecha creacion', new Date(order.createdAt).toLocaleDateString('es-CR')],
            ['Fecha requerida', order.requiredAt
                ? new Date(order.requiredAt).toLocaleDateString('es-CR')
                : '-'],
            ['Estado', this.getStatus(order.statusId)],
            ['Monto total', `${Number(order.total).toLocaleString('es-CR')}`],
        ];

        doc.font('Helvetica').fontSize(11);
        for (const [label, value] of info) {
            doc.text(`${label}: `, { continued: true }).font('Helvetica-Bold').text(value);
            doc.font('Helvetica');
        }

        // Líneas
        doc.moveDown();
        doc.fontSize(12).font('Helvetica-Bold').text('Lineas de la orden');
        doc.moveTo(50, doc.y).lineTo(550, doc.y).stroke();
        doc.moveDown(0.5);

        if (order.lines.length === 0) {
            doc.font('Helvetica').fontSize(11).text('Sin lineas registradas.');
        } else {
            doc.font('Helvetica').fontSize(11);
            for (const line of order.lines) {
                doc.text(`- SKU: ${line.sku} - ${line.description}`);
                doc.text(
                    `  Costo: ${Number(line.cost).toLocaleString('es-CR')}` +
                    `  |  Ordenado: ${line.quantityOrdered}` +
                    `  |  Recibido: ${line.quantityReceived}` +
                    `  |  Devolucion: ${line.quantityReturned}` +
                    `  |  Backorder: ${line.quantityBackorder}`,
                );
                doc.moveDown(0.3);
            }
        }

        doc.end();
    }

}

