import { Controller, Get, Query, Request, Res, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { AccountStatementsService } from './account-statements.service';
import { Response } from 'express';

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

    @Get('pdf')
    downloadPdf(
        @Request() req,
        @Res() res: Response,
        @Query('from') from?: string,
        @Query('to') to?: string,
    ) {
        const fromDate = from ? new Date(from) : undefined;
        const toDate = to ? new Date(to) : undefined;
        return this.service.downloadPdfStatement(req.user.providerId, fromDate, toDate, res);
    }

    @Get('excel')
    downloadExcel(
        @Request() req,
        @Res() res: Response,
        @Query('from') from?: string,
        @Query('to') to?: string,
    ) {
        const fromDate = from ? new Date(from) : undefined;
        const toDate = to ? new Date(to) : undefined;
        return this.service.downloadExcelStatement(req.user.providerId, fromDate, toDate, res);
    }
}