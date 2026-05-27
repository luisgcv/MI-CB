import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { BranchEntity } from '../../database/entities/branch.entity';
import { CabysEntity } from '../../database/entities/cabys.entity';
import { ProductBranchEntity } from '../../database/entities/product-branch.entity';
import { ProductCabysEntity } from '../../database/entities/product-cabys.entity';
import { ProductEntity } from '../../database/entities/product.entity';
import { ProductImageEntity } from '../../database/entities/product-image.entity';
import { ProductMotiveEntity } from '../../database/entities/product-motive.entity';
import { ProductStateHistoryEntity } from '../../database/entities/product-state-history.entity';
import { ProductStatusEntity } from '../../database/entities/product-status.entity';
import { ProviderEntity } from '../../database/entities/provider.entity';
import { ReturnConditionEntity } from '../../database/entities/return-condition.entity';
import { UnitMeasureEntity } from '../../database/entities/unit-measure.entity';
import { ProductsController } from './products.controller';
import { ProductsService } from './products.service';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      ProductEntity,
      ProductImageEntity,
      ProductBranchEntity,
      ProductCabysEntity,
      ProductMotiveEntity,
      ProductStateHistoryEntity,
      UnitMeasureEntity,
      ReturnConditionEntity,
      ProductStatusEntity,
      BranchEntity,
      CabysEntity,
      ProviderEntity,
    ]),
  ],
  controllers: [ProductsController],
  providers: [ProductsService],
})
export class ProductsModule {}