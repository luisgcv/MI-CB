import { Body, Controller, Get, Post, Req, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { CreateMeetingDto } from './dto/create-meeting.dto';
import { MeetingsService } from './meetings.service';

@Controller('reuniones')
@UseGuards(JwtAuthGuard)
export class MeetingsController {
  constructor(private readonly meetingsService: MeetingsService) {}

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
}

