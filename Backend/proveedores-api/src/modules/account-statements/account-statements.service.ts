import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Between, Repository } from 'typeorm';
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
            .filter((d) => d.status.description === 'Pendiente')
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
}