import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AccountStatementsController } from './account-statements.controller';
import { AccountStatementsService } from './account-statements.service';
import { DocumentEntity } from '../../database/entities/account-statement.entity';
import { DocumentLineEntity } from '../../database/entities/account-statement-line.entity';
import { DocumentTypeEntity } from '../../database/entities/account-statement-type.entity';
import { DocumentStatusEntity } from '../../database/entities/account-statement-status.entity';

@Module({
    imports: [
        TypeOrmModule.forFeature([
            DocumentEntity,
            DocumentLineEntity,
            DocumentTypeEntity,
            DocumentStatusEntity,
        ]),
    ],
    controllers: [AccountStatementsController],
    providers: [AccountStatementsService],
})
export class AccountStatementsModule { }