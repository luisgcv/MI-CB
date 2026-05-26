import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { PurchaseOrdersController } from './purchase-orders.controller';
import { PurchaseOrdersService } from './purchase-orders.service';
import { PurchaseOrderEntity } from '../../database/entities/purchase-order.entity';
import { PurchaseOrderLineEntity } from '../../database/entities/purchase-order-line.entity';

@Module({
    imports: [
        TypeOrmModule.forFeature([PurchaseOrderEntity, PurchaseOrderLineEntity]),
    ],
    controllers: [PurchaseOrdersController],
    providers: [PurchaseOrdersService],
})
export class PurchaseOrdersModule { }