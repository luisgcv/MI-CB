import { Controller, Get, Param, Request, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { PurchaseOrdersService } from './purchase-orders.service';

@UseGuards(JwtAuthGuard)
@Controller('purchase-orders')
export class PurchaseOrdersController {
    constructor(private readonly purchaseOrdersService: PurchaseOrdersService) { }

    @Get()
    getOrders(@Request() req) {
        return this.purchaseOrdersService.getOrders(req.user.providerId);
    }

    @Get(':id')
    getDetail(@Param('id') id: string, @Request() req) {
        return this.purchaseOrdersService.getOrderDetail(id, req.user.providerId);
    }
}