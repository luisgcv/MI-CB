import {
  Controller,
  Get,
  Param,
  Query,
  Res,
  Request,
  UseGuards,
} from '@nestjs/common';
import { Response } from 'express';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { DynamicsService } from './dynamics.service';

@UseGuards(JwtAuthGuard)
@Controller('dynamics')
export class DynamicsController {
  constructor(
    private readonly dynamicsService: DynamicsService,
  ) {}

  @Get()
  getDynamics(
    @Request() req,
    @Query('startDate') startDate?: string,
    @Query('endDate') endDate?: string,
  ) {
    return this.dynamicsService.getDynamics(
      req.user.providerId,
      startDate,
      endDate,
    );
  }

  @Get(':id')
  getDetail(
    @Param('id') id: string,
    @Request() req,
  ) {
    return this.dynamicsService.getDynamicDetail(
      id,
      req.user.providerId,
    );
  }
  
}