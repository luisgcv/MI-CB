import { Controller, Get, Query, Request, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { AccountStatementsService } from './account-statements.service';

@UseGuards(JwtAuthGuard)
@Controller('account-statements')
export class AccountStatementsController {
    constructor(private readonly service: AccountStatementsService) { }

    @Get()
    getDocuments(
        @Request() req,
        @Query('from') from?: string,
        @Query('to') to?: string,
    ) {
        const fromDate = from ? new Date(from) : undefined;
        const toDate = to ? new Date(to) : undefined;
        return this.service.getDocuments(req.user.providerId, fromDate, toDate);
    }
}