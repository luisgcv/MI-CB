import {
  Controller,
  Get,
  Param,
  Request,
  UseGuards,
} from '@nestjs/common';

import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';

import { DynamicsService } from './dynamics.service';
@UseGuards(JwtAuthGuard)
@Controller('dynamics')
export class DynamicsController {
  constructor(
    private readonly dynamicsService: DynamicsService,
  ) {}

  @Get()
  getDynamics(@Request() req) {
    return this.dynamicsService.getDynamics(
      req.user.providerId,
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