import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { ContractEntity } from '../../database/entities/contract.entity';
import { ContractsService } from './contracts.service';
import { ContractsController } from './contracts.controller';
import { BranchEntity } from '../../database/entities/branch.entity';
import { ContractLineEntity } from '../../database/entities/contract-line.entity';
import { ContractChargeEntity } from '../../database/entities/contract-charge.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      ContractEntity,
      BranchEntity,
      ContractLineEntity,
      ContractChargeEntity,
    ]),
  ],
  controllers: [ContractsController],
  providers: [ContractsService],
})
export class ContractsModule {}