import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Between, Repository } from 'typeorm';
import { Response } from 'express';
import PDFDocument from 'pdfkit';
import * as ExcelJS from 'exceljs';
import { DocumentEntity } from '../../database/entities/account-statement.entity';

@Injectable()
export class AccountStatementsService {
    constructor(
        @InjectRepository(DocumentEntity)
        private documentRepository: Repository<DocumentEntity>,
    ) { }

    async getDocuments(
        providerId: string,
        fromDate?: Date,
        toDate?: Date,
    ) {
        const where: any = { providerId };

        if (fromDate && toDate)
            where.documentDate = Between(fromDate, toDate);

        const documents = await this.documentRepository.find({
            where,
            relations: ['type', 'status'],
            order: { documentDate: 'DESC' },
        });

        const totalDebt = documents
            .filter((d) => d.status.description === 'Pendiente' || d.status.description === 'Vencido')
            .reduce((sum, d) => sum + Number(d.amount), 0);

        return {
            totalDebt,
            documents: documents.map((d) => this.mapSummary(d)),
        };
    }

    private mapSummary(d: DocumentEntity) {
        return {
            id: d.id,
            type: d.type?.description ?? '-',
            status: d.status?.description ?? '-',
            documentDate: d.documentDate,
            paymentDate: d.paymentDate,
            amount: Number(d.amount),
        };
    }

    async downloadPdfStatement(providerId: string, fromDate: Date | undefined, toDate: Date | undefined, res: Response) {
        const where: any = { providerId };
        if (fromDate && toDate)
            where.documentDate = Between(fromDate, toDate);

        const documents = await this.documentRepository.find({
            where,
            relations: ['type', 'status', 'lines'],
            order: { documentDate: 'DESC' },
        });

        const totalDebt = documents
            .filter((d) => d.status.description === 'Pendiente')
            .reduce((sum, d) => sum + Number(d.amount), 0);

        res.setHeader('Content-Type', 'application/pdf');
        res.setHeader('Content-Disposition', 'attachment; filename="estado-de-cuenta.pdf"');

        const doc = new PDFDocument({ margin: 50 });
        doc.pipe(res);

        // Encabezado
        doc.fontSize(18).font('Helvetica-Bold').text('Estado de Cuenta', { align: 'center' });
        doc.moveDown(0.5);
        doc.fontSize(10).font('Helvetica').fillColor('#888888')
            .text('Informacion actualizada al cierre del dia anterior.', { align: 'center' });
        doc.fillColor('#000000');
        doc.moveDown();

        // Total de deuda
        doc.fontSize(12).font('Helvetica-Bold').text('Resumen');
        doc.moveTo(50, doc.y).lineTo(550, doc.y).stroke();
        doc.moveDown(0.5);
        doc.font('Helvetica').fontSize(11)
            .text('Total pendiente de pago: ', { continued: true })
            .font('Helvetica-Bold')
            .text(`${totalDebt.toLocaleString('es-CR')}`);
        doc.moveDown();

        // Documentos
        doc.fontSize(12).font('Helvetica-Bold').text('Documentos');
        doc.moveTo(50, doc.y).lineTo(550, doc.y).stroke();
        doc.moveDown(0.5);

        if (documents.length === 0) {
            doc.font('Helvetica').fontSize(11).text('Sin documentos registrados.');
        } else {
            for (const d of documents) {
                doc.font('Helvetica-Bold').fontSize(11).text(`${d.id} - ${d.type?.description ?? '-'}`);
                doc.font('Helvetica').fontSize(10);
                doc.text(
                    `Estado: ${d.status?.description ?? '-'}` +
                    `  |  Fecha doc: ${new Date(d.documentDate).toLocaleDateString('es-CR')}` +
                    `  |  Fecha pago: ${new Date(d.paymentDate).toLocaleDateString('es-CR')}` +
                    `  |  Monto: ${Number(d.amount).toLocaleString('es-CR')}`
                );

                if (d.lines?.length > 0) {
                    for (const line of d.lines) {
                        doc.text(`  - ${line.description}: ${Number(line.amount).toLocaleString('es-CR')}`);
                    }
                }
                doc.moveDown(0.5);
            }
        }

        doc.end();
    }

    async downloadExcelStatement(providerId: string, fromDate: Date | undefined, toDate: Date | undefined, res: Response) {
        const where: any = { providerId };
        if (fromDate && toDate)
            where.documentDate = Between(fromDate, toDate);

        const documents = await this.documentRepository.find({
            where,
            relations: ['type', 'status', 'lines'],
            order: { documentDate: 'DESC' },
        });

        const totalDebt = documents
            .filter((d) => d.status.description === 'Pendiente')
            .reduce((sum, d) => sum + Number(d.amount), 0);

        const workbook = new ExcelJS.Workbook();
        const sheet = workbook.addWorksheet('Estado de Cuenta');

        // Aviso de delay
        sheet.addRow(['Informacion actualizada al cierre del dia anterior.']);
        sheet.getRow(1).font = { italic: true, color: { argb: 'FF888888' } };
        sheet.addRow([]);

        // Total deuda
        sheet.addRow(['Total pendiente de pago:', totalDebt]);
        sheet.getRow(3).font = { bold: true };
        sheet.addRow([]);

        // Encabezados de tabla
        const headerRow = sheet.addRow([
            'Consecutivo',
            'Tipo',
            'Estado',
            'Fecha documento',
            'Fecha pago',
            'Monto',
        ]);
        headerRow.font = { bold: true };
        headerRow.eachCell((cell) => {
            cell.fill = {
                type: 'pattern',
                pattern: 'solid',
                fgColor: { argb: 'FF46A441' },
            };
            cell.font = { bold: true, color: { argb: 'FFFFFFFF' } };
            cell.alignment = { horizontal: 'center' };
        });

        // Datos
        for (const d of documents) {
            sheet.addRow([
                d.id,
                d.type?.description ?? '-',
                d.status?.description ?? '-',
                new Date(d.documentDate).toLocaleDateString('es-CR'),
                new Date(d.paymentDate).toLocaleDateString('es-CR'),
                Number(d.amount),
            ]);
        }

        // Ancho de columnas
        sheet.columns = [
            { width: 20 },
            { width: 20 },
            { width: 15 },
            { width: 18 },
            { width: 18 },
            { width: 15 },
        ];

        res.setHeader('Content-Type', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
        res.setHeader('Content-Disposition', 'attachment; filename="estado-de-cuenta.xlsx"');

        await workbook.xlsx.write(res);
        res.end();
    }

}