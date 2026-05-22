import { BadRequestException, Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { In, Repository } from 'typeorm';
import { CreateMeetingDto } from './dto/create-meeting.dto';
import { MeetingEntity } from '../../database/entities/meeting.entity';
import { MeetingTypeEntity } from '../../database/entities/meeting-type.entity';
import { MeetingStateEntity } from '../../database/entities/meeting-state.entity';
import { ProviderUserDepartmentEntity } from '../../database/entities/provider-user-department.entity';
import { DepartmentEntity } from '../../database/entities/department.entity';

@Injectable()
export class MeetingsService {
  private readonly meetingTypeMap: Record<string, number> = {
    comercial: 1,
    técnica: 2,
    tecnica: 2,
    administrativa: 3,
    otro: 4,
  };

  constructor(
    @InjectRepository(MeetingEntity)
    private readonly meetingRepository: Repository<MeetingEntity>,
    @InjectRepository(MeetingTypeEntity)
    private readonly meetingTypeRepository: Repository<MeetingTypeEntity>,
    @InjectRepository(MeetingStateEntity)
    private readonly meetingStateRepository: Repository<MeetingStateEntity>,
    @InjectRepository(ProviderUserDepartmentEntity)
    private readonly pduRepository: Repository<ProviderUserDepartmentEntity>,
    @InjectRepository(DepartmentEntity)
    private readonly departmentRepository: Repository<DepartmentEntity>,
  ) {}

  async getMeetingTypes() {
    const meetingTypes = await this.meetingTypeRepository.find({
      order: {
        meetingTypeId: 'ASC',
      },
    });

    return meetingTypes.map((meetingType) => ({
      id: meetingType.meetingTypeId,
      descripcion: meetingType.description,
    }));
  }

    async getDepartments() {
    const departments = await this.departmentRepository.find();

    return departments.map((dept) => ({
        id: dept.departmentId,
        nombre: dept.departmentName,
    }));
    }

  async createMeeting(identificationId: string, dto: CreateMeetingDto) {
    const pdu = await this.pduRepository.findOne({ where: { identificationId } });
    if (!pdu) {
      throw new BadRequestException('No se encontró la empresa y el departamento del usuario.');
    }

    const startDate = new Date(dto.fechaHoraInicio);
    const endDate = new Date(dto.fechaHoraHasta);

    if (Number.isNaN(startDate.getTime()) || Number.isNaN(endDate.getTime())) {
      throw new BadRequestException('Fecha u hora inválida.');
    }

    const meetingTypeId = this.getMeetingTypeId(dto.tipoReunion);
    const meetingStateId = this.getMeetingStateId(dto.isDraft ?? false);

    await this.ensureMeetingTypeExists(meetingTypeId, dto.tipoReunion);
    await this.ensureMeetingStateExists(meetingStateId, dto.isDraft ?? false);

    const meeting = this.meetingRepository.create({
      providerId: pdu.providerId,
      meetingTypeId,
      departmentId: dto.departmentId,
      meetingStateId,
      reason: dto.motivo,
      observations: dto.observaciones ?? '',
      startDateTime: startDate,
      endDateTime: endDate,
    });

    return this.meetingRepository.save(meeting);
  }

  async getMeetings(identificationId: string) {
    const pdu = await this.pduRepository.findOne({ where: { identificationId } });
    if (!pdu) {
      throw new BadRequestException('No se encontró la empresa y el departamento del usuario.');
    }

    const meetings = await this.meetingRepository.find({
    where: {
        providerId: pdu.providerId,
    },
    order: {
        startDateTime: 'DESC',
    },
    });

    if (meetings.length === 0) {
      return [];
    }

    const meetingTypeIds = Array.from(new Set(meetings.map((meeting) => meeting.meetingTypeId)));
    const meetingStateIds = Array.from(new Set(meetings.map((meeting) => meeting.meetingStateId)));

    const meetingTypes = await this.meetingTypeRepository.find({
      where: {
        meetingTypeId: In(meetingTypeIds),
      },
    });

    const meetingStates = await this.meetingStateRepository.find({
      where: {
        meetingStateId: In(meetingStateIds),
      },
    });

    const typeMap = Object.fromEntries(meetingTypes.map((type) => [type.meetingTypeId, type.description]));
    const stateMap = Object.fromEntries(meetingStates.map((state) => [state.meetingStateId, state.description]));

    return meetings.map((meeting) => ({
      id: meeting.id,
      tipoReunion: typeMap[meeting.meetingTypeId] ?? 'Otro',
      estadoReunion: stateMap[meeting.meetingStateId] ?? 'Desconocido',
      motivo: meeting.reason,
      observaciones: meeting.observations,
      fechaHoraInicio: meeting.startDateTime,
      fechaHoraHasta: meeting.endDateTime,
    }));
  }

  private async ensureMeetingTypeExists(meetingTypeId: number, tipoReunion: string) {
    const existing = await this.meetingTypeRepository.findOne({ where: { meetingTypeId } });
    if (!existing) {
      const entity = this.meetingTypeRepository.create({
        meetingTypeId,
        description: tipoReunion.trim(),
      });
      await this.meetingTypeRepository.save(entity);
    }
  }

  private async ensureMeetingStateExists(meetingStateId: number, isDraft: boolean) {
    const existing = await this.meetingStateRepository.findOne({ where: { meetingStateId } });
    if (!existing) {
      const entity = this.meetingStateRepository.create({
        meetingStateId,
        description: isDraft ? 'Borrador' : 'Programada',
      });
      await this.meetingStateRepository.save(entity);
    }
  }

  private getMeetingTypeId(tipoReunion: string): number {
    return this.meetingTypeMap[tipoReunion.trim().toLowerCase()] ?? 1;
  }

  private getMeetingStateId(isDraft: boolean): number {
    return isDraft ? 2 : 1;
  }
}
