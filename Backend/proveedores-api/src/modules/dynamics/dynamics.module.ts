import { Module } from '@nestjs/common';

import { TypeOrmModule } from '@nestjs/typeorm';

import { DynamicsController } from './dynamics.controller';
import { DynamicsService } from './dynamics.service';

import { DynamicEntity } from '../../database/entities/dynamic.entity';
import { DynamicLineEntity } from '../../database/entities/dynamic-line.entity';
import { DynamicChargeEntity } from '../../database/entities/dynamic-charge.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      DynamicEntity,
      DynamicLineEntity,
      DynamicChargeEntity,
    ]),
  ],

  controllers: [DynamicsController],

  providers: [DynamicsService],
})
export class DynamicsModule {}