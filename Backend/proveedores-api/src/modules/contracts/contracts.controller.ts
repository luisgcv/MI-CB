import {
  Controller,
  Get,
  Param,
  Res,
  UseGuards,
  Request,
} from '@nestjs/common';
import { Response } from 'express';
import { ContractsService } from './contracts.service';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';

@UseGuards(JwtAuthGuard)
@Controller('contracts')
export class ContractsController {
  constructor(private readonly contractsService: ContractsService) {}

  @Get()
  getContracts(@Request() req) {
    return this.contractsService.getContracts(req.user.providerId);
  }

  @Get(':consecutivo')
  getDetail(@Param('consecutivo') consecutivo: string, @Request() req) {
    return this.contractsService.getContractDetail(
      consecutivo,
      req.user.providerId,
    );
  }

  @Get(':consecutivo/pdf')
  downloadPdf(
    @Param('consecutivo') consecutivo: string,
    @Request() req,
    @Res() res: Response,
  ) {
    return this.contractsService.downloadPdf(
      consecutivo,
      req.user.providerId,
      res,
    );
  }
}
