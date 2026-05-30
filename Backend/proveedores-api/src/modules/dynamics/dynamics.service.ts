import {
  Injectable,
  NotFoundException,
} from '@nestjs/common';

import { InjectRepository } from '@nestjs/typeorm';
import { Repository, Between } from 'typeorm';

import { DynamicEntity } from '../../database/entities/dynamic.entity';
import { Response } from 'express';
import PDFDocument from 'pdfkit';
import * as ExcelJS from 'exceljs';

@Injectable()
export class DynamicsService {
  constructor(
    @InjectRepository(DynamicEntity)
    private dynamicRepository: Repository<DynamicEntity>,
  ) {}

  async getDynamics(
    providerId: string,
    startDate?: string,
    endDate?: string,
  ) {
    const where: any = {
      providerId,
    };

    if (startDate && endDate) {
      where.startDate = Between(
        new Date(startDate),
        new Date(endDate),
      );
    }

    const dynamics = await this.dynamicRepository.find({
      where,
      relations: ['branch'],
      order: {
        startDate: 'DESC',
      },
    });

    return dynamics.map((d) =>
      this.mapSummary(d),
    );
  }

  async getDynamicDetail(
    id: string,
    providerId: string,
  ) {
    const dynamic =
      await this.dynamicRepository.findOne({
        where: { id, providerId },
        relations: [
          'branch',
          'lines',
          'charges',
        ],
      });

    if (!dynamic) {
      throw new NotFoundException(
        'Dinámica no encontrada',
      );
    }
    //Participacion: consultar resultados
    const totalQuantity = dynamic.lines.reduce(
      (sum, line) => sum + Number(line.quantity),
      0,
    );

    const totalApplied = dynamic.lines.reduce(
      (sum, line) => sum + Number(line.amount),
      0,
    );

    const averageAmount =
      dynamic.lines.length > 0
        ? totalApplied / dynamic.lines.length
        : 0;

    return {
      ...this.mapSummary(dynamic),

      lines: dynamic.lines.map((l) => ({
        sku: l.sku,
        description: l.description,
        quantity: Number(l.quantity),
        amount: Number(l.amount),
      })),

      charges: dynamic.charges.map((c) => ({
        document: c.document,
        amount: Number(c.amount),
        date: c.date,
      })),
    //paricipacion
      results: {
        totalArticles: dynamic.lines.length,
        totalQuantity,
        totalApplied,
        averageAmount,
      },
    };
  }

    private mapSummary(d: DynamicEntity) {

        const isActive =
        new Date(d.endDate) >= new Date();

        return {
            id: d.id,
            type: d.tipoDinamica,

            branch:
            d.branch?.nombreSucursal ?? '-',

            startDate: d.startDate,
            endDate: d.endDate,

            total: Number(
            d.totalAmount,
            ),

            isActive,

            status:
            isActive
                ? 'Activa'
                : 'Finalizada',
        };
    }
}