import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { MeetingsController } from './meetings.controller';
import { MeetingsService } from './meetings.service';
import { MeetingEntity } from '../../database/entities/meeting.entity';
import { MeetingTypeEntity } from '../../database/entities/meeting-type.entity';
import { MeetingStateEntity } from '../../database/entities/meeting-state.entity';
import { ProviderUserDepartmentEntity } from '../../database/entities/provider-user-department.entity';
import { DepartmentEntity } from '../../database/entities/department.entity';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      MeetingEntity,
      MeetingTypeEntity,
      MeetingStateEntity,
      ProviderUserDepartmentEntity,
      DepartmentEntity,
    ]),
  ],
  controllers: [MeetingsController],
  providers: [MeetingsService],
})
export class MeetingsModule {}
