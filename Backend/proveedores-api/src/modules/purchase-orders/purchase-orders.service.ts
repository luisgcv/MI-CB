import { Injectable, NotFoundException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
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
}