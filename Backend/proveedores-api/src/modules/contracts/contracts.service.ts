import { Injectable, NotFoundException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Response } from 'express';
import PDFDocument from 'pdfkit';
import { ContractEntity } from '../../database/entities/contract.entity';

@Injectable()
export class ContractsService {
  constructor(
    @InjectRepository(ContractEntity)
    private contractRepository: Repository<ContractEntity>,
  ) {}

  async getContracts(idProveedor: string) {
    const contracts = await this.contractRepository.find({
      where: { idProveedor },
      relations: ['sucursal'],
      order: { fechaInicio: 'DESC' },
    });

    return contracts.map((c) => this.mapSummary(c));
  }

  async getContractDetail(consecutivo: string, idProveedor: string) {
    const contract = await this.contractRepository.findOne({
      where: { consecutivo, idProveedor },
      relations: ['sucursal', 'lineas', 'cobros'],
    });

    if (!contract) throw new NotFoundException('Contrato no encontrado');

    return {
      ...this.mapSummary(contract),
      lineas: contract.lineas.map((l) => ({
        id: l.id,
        descripcion: l.descripcion,
        cantidad: Number(l.cantidad),
        monto: Number(l.monto),
      })),
      cobros: contract.cobros.map((c) => ({
        id: c.id,
        documento: c.documento,
        monto: Number(c.monto),
        fecha: c.fecha,
      })),
    };
  }

  async downloadPdf(consecutivo: string, idProveedor: string, res: Response) {
    const contract = await this.contractRepository.findOne({
      where: { consecutivo, idProveedor },
      relations: ['sucursal', 'lineas', 'cobros'],
    });

    if (!contract) throw new NotFoundException('Contrato no encontrado');

    res.setHeader('Content-Type', 'application/pdf');
    res.setHeader(
      'Content-Disposition',
      `attachment; filename="contrato-${consecutivo}.pdf"`,
    );

    const doc = new PDFDocument({ margin: 50 });
    doc.pipe(res);

    // Encabezado
    doc
      .fontSize(18)
      .font('Helvetica-Bold')
      .text('Detalle de Contrato', { align: 'center' });
    doc.moveDown();

    // Información general
    doc.fontSize(12).font('Helvetica-Bold').text('Información general');
    doc.moveTo(50, doc.y).lineTo(550, doc.y).stroke();
    doc.moveDown(0.5);

    const info = [
      ['Consecutivo', contract.consecutivo],
      ['Tipo de contrato', contract.tipoContrato],
      ['Sucursal', contract.sucursal?.nombreSucursal ?? '-'],
      ['Fecha inicio', contract.fechaInicio.toLocaleDateString('es-CR')],
      ['Fecha vence', contract.fechaHasta.toLocaleDateString('es-CR')],
      ['Estado', this.getEstado(contract)],
      [
        'Monto mensual',
        `₡${Number(contract.montoMensual).toLocaleString('es-CR')}`,
      ],
    ];

    doc.font('Helvetica').fontSize(11);
    for (const [label, value] of info) {
      doc
        .text(`${label}: `, { continued: true })
        .font('Helvetica-Bold')
        .text(value);
      doc.font('Helvetica');
    }

    // Líneas
    doc.moveDown();
    doc.fontSize(12).font('Helvetica-Bold').text('Líneas del contrato');
    doc.moveTo(50, doc.y).lineTo(550, doc.y).stroke();
    doc.moveDown(0.5);

    if (contract.lineas.length === 0) {
      doc.font('Helvetica').fontSize(11).text('Sin líneas registradas.');
    } else {
      doc.font('Helvetica').fontSize(11);
      for (const linea of contract.lineas) {
        doc.text(
          `• ${linea.descripcion} — Cantidad: ${linea.cantidad}, Monto: ₡${Number(linea.monto).toLocaleString('es-CR')}`,
        );
      }
    }

    // Cobros
    doc.moveDown();
    doc.fontSize(12).font('Helvetica-Bold').text('Cobros aplicados');
    doc.moveTo(50, doc.y).lineTo(550, doc.y).stroke();
    doc.moveDown(0.5);

    if (contract.cobros.length === 0) {
      doc.font('Helvetica').fontSize(11).text('Sin cobros registrados.');
    } else {
      doc.font('Helvetica').fontSize(11);
      for (const cobro of contract.cobros) {
        doc.text(
          `• Doc: ${cobro.documento} — ₡${Number(cobro.monto).toLocaleString('es-CR')} — ${new Date(cobro.fecha).toLocaleDateString('es-CR')}`,
        );
      }
    }

    doc.end();
  }

  private mapSummary(c: ContractEntity) {
    const hoy = new Date();
    const inicio = new Date(c.fechaInicio);
    const hasta = new Date(c.fechaHasta);
    const totalDias = Math.ceil(
      (hasta.getTime() - inicio.getTime()) / 86400000,
    );
    const diasTranscurridos = Math.max(
      0,
      Math.ceil((hoy.getTime() - inicio.getTime()) / 86400000),
    );
    const diasRestantes = Math.max(
      0,
      Math.ceil((hasta.getTime() - hoy.getTime()) / 86400000),
    );
    const porcentaje =
      totalDias > 0
        ? Math.min(100, Math.round((diasTranscurridos / totalDias) * 100))
        : 100;

    return {
      consecutivo: c.consecutivo,
      tipoContrato: c.tipoContrato,
      sucursal: c.sucursal?.nombreSucursal ?? '-',
      fechaInicio: c.fechaInicio,
      fechaHasta: c.fechaHasta,
      montoMensual: Number(c.montoMensual),
      estado: this.getEstado(c),
      diasRestantes,
      porcentajeTranscurrido: porcentaje,
    };
  }

  private getEstado(c: ContractEntity): string {
    return new Date(c.fechaHasta) >= new Date() ? 'Vigente' : 'Finalizado';
  }
}
