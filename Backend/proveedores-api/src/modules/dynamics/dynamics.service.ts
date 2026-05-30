import {
  Injectable,
  NotFoundException,
} from '@nestjs/common';

import { InjectRepository } from '@nestjs/typeorm';
import { Repository, Between } from 'typeorm';

import { DynamicEntity } from '../../database/entities/dynamic.entity';

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