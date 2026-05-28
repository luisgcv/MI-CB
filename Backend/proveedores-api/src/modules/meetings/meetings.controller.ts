import { Body, Controller, Get, Delete, Param, Patch, Post, Req, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { CreateMeetingDto } from './dto/create-meeting.dto';
import { MeetingsService } from './meetings.service';

@Controller('reuniones')
@UseGuards(JwtAuthGuard)
export class MeetingsController {
  constructor(private readonly meetingsService: MeetingsService) { }

  @Get('tipos')
  getMeetingTypes() {
    return this.meetingsService.getMeetingTypes();
  }

  @Get('departamentos/disponibles')
  getDepartments(@Req() req: any) {
    return this.meetingsService.getDepartments();
  }

  @Get()
  getMeetings(@Req() req: any) {
    const identificationId: string = req.user?.sub;
    return this.meetingsService.getMeetings(identificationId);
  }

  @Post()
  createMeeting(@Req() req: any, @Body() dto: CreateMeetingDto) {
    const identificationId: string = req.user?.sub;
    return this.meetingsService.createMeeting(identificationId, dto);
  }

  @Patch(':id/enviar')
  sendDraft(@Req() req: any, @Param('id') id: string) {
    const identificationId: string = req.user?.sub;
    return this.meetingsService.sendDraft(identificationId, Number(id));
  }

  @Get(':id')
  getMeetingById(@Req() req: any, @Param('id') id: string) {
    const identificationId: string = req.user?.sub;
    return this.meetingsService.getMeetingById(identificationId, Number(id));
  }

  @Patch(':id')
  updateMeeting(
    @Req() req: any,
    @Param('id') id: string,
    @Body() dto: CreateMeetingDto,
  ) {
    const identificationId: string = req.user?.sub;
    return this.meetingsService.updateDraft(identificationId, Number(id), dto);
  }

  @Patch(':id/descartar')
  discardDraft(@Req() req: any, @Param('id') id: string) {
    const identificationId: string = req.user?.sub;
    return this.meetingsService.discardDraft(identificationId, Number(id));
  }

  @Patch(':id/cancelar')
  cancelMeeting(@Req() req: any, @Param('id') id: string) {
    const identificationId: string = req.user?.sub;
    return this.meetingsService.cancelMeeting(identificationId, Number(id));
  }
}

