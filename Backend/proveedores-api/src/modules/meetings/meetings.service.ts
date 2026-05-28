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
  ) { }

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

    const meetingType = await this.meetingTypeRepository.findOne({
      where: { description: dto.tipoReunion },
    });

    if (!meetingType) {
      throw new BadRequestException('Tipo de reunión no válido.');
    }

    const stateDescription = dto.isDraft ? 'Borrador' : 'Pendiente';

    const meetingState = await this.meetingStateRepository.findOne({
      where: { description: stateDescription },
    });

    if (!meetingState) {
      throw new BadRequestException('Estado de reunión no configurado.');
    }

    const meetingTypeId = meetingType.meetingTypeId;
    const meetingStateId = meetingState.meetingStateId;

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
    const departmentIds = Array.from(new Set(meetings.map((meeting) => meeting.departmentId)));

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

    const departments = await this.departmentRepository.find({
      where: {
        departmentId: In(departmentIds),
      },
    });

    const typeMap = Object.fromEntries(meetingTypes.map((type) => [type.meetingTypeId, type.description]));
    const stateMap = Object.fromEntries(meetingStates.map((state) => [state.meetingStateId, state.description]));
    const departmentMap = Object.fromEntries(departments.map((department) => [department.departmentId, department.departmentName]));

    return meetings.map((meeting) => ({
      id: meeting.id,
      tipoReunion: typeMap[meeting.meetingTypeId] ?? 'Otro',
      estadoReunion: stateMap[meeting.meetingStateId] ?? 'Desconocido',
      departamento: departmentMap[meeting.departmentId] ?? '',
      motivo: meeting.reason,
      observaciones: meeting.observations,
      fechaHoraInicio: meeting.startDateTime,
      fechaHoraHasta: meeting.endDateTime,
    }));
  }

  async getMeetingById(identificationId: string, meetingId: number) {
    const pdu = await this.pduRepository.findOne({
      where: { identificationId },
    });

    if (!pdu) {
      throw new BadRequestException(
        'El usuario no tiene proveedor asociado.',
      );
    }

    const meeting = await this.meetingRepository.findOne({
      where: {
        id: meetingId,
        providerId: pdu.providerId,
      },
    });

    if (!meeting) {
      throw new BadRequestException(
        'La reunión no existe o no pertenece al proveedor.',
      );
    }

    const meetingType = await this.meetingTypeRepository.findOne({
      where: {
        meetingTypeId: meeting.meetingTypeId,
      },
    });

    const meetingState = await this.meetingStateRepository.findOne({
      where: {
        meetingStateId: meeting.meetingStateId,
      },
    });

    return {
      id: meeting.id,
      departmentId: meeting.departmentId,
      tipoReunion: meetingType?.description ?? '',
      estadoReunion: meetingState?.description ?? '',
      motivo: meeting.reason,
      observaciones: meeting.observations,
      fechaHoraInicio: meeting.startDateTime,
      fechaHoraHasta: meeting.endDateTime,
    };
  }



  async sendDraft(identificationId: string, meetingId: number) {
    const pdu = await this.pduRepository.findOne({
      where: { identificationId },
    });

    if (!pdu) {
      throw new BadRequestException(
        'El usuario no tiene proveedor asociado.',
      );
    }

    const meeting = await this.meetingRepository.findOne({
      where: {
        id: meetingId,
        providerId: pdu.providerId,
      },
    });

    if (!meeting) {
      throw new BadRequestException(
        'La reunión no existe o no pertenece al proveedor.',
      );
    }

    const pendingState = await this.meetingStateRepository.findOne({
      where: { description: 'Pendiente' },
    });

    if (!pendingState) {
      throw new BadRequestException(
        'Estado Pendiente no configurado.',
      );
    }

    meeting.meetingStateId = pendingState.meetingStateId;

    return this.meetingRepository.save(meeting);
  }

  async updateDraft(
    identificationId: string,
    meetingId: number,
    dto: CreateMeetingDto,
  ) {
    const pdu = await this.pduRepository.findOne({
      where: { identificationId },
    });

    if (!pdu) {
      throw new BadRequestException(
        'El usuario no tiene proveedor asociado.',
      );
    }

    const meeting = await this.meetingRepository.findOne({
      where: {
        id: meetingId,
        providerId: pdu.providerId,
      },
    });

    if (!meeting) {
      throw new BadRequestException(
        'La reunión no existe o no pertenece al proveedor.',
      );
    }

    const currentState = await this.meetingStateRepository.findOne({
      where: {
        meetingStateId: meeting.meetingStateId,
      },
    });

    if (!currentState) {
      throw new BadRequestException(
        'Estado de reunión inválido.',
      );
    }

    if (
      currentState.description.toLowerCase() !== 'borrador'
    ) {
      throw new BadRequestException(
        'Solo se pueden editar reuniones en borrador.',
      );
    }

    const meetingType = await this.meetingTypeRepository.findOne({
      where: {
        description: dto.tipoReunion,
      },
    });

    if (!meetingType) {
      throw new BadRequestException(
        'Tipo de reunión no válido.',
      );
    }

    meeting.reason = dto.motivo;
    meeting.observations = dto.observaciones ?? '';
    meeting.departmentId = dto.departmentId;
    meeting.meetingTypeId = meetingType.meetingTypeId;
    meeting.startDateTime = new Date(dto.fechaHoraInicio);
    meeting.endDateTime = new Date(dto.fechaHoraHasta);

    return this.meetingRepository.save(meeting);
  }

  async discardDraft(identificationId: string, meetingId: number) {
    const pdu = await this.pduRepository.findOne({
      where: { identificationId },
    });

    if (!pdu) {
      throw new BadRequestException('El usuario no tiene proveedor asociado.');
    }

    const meeting = await this.meetingRepository.findOne({
      where: {
        id: meetingId,
        providerId: pdu.providerId,
      },
    });

    if (!meeting) {
      throw new BadRequestException('La reunión no existe o no pertenece al proveedor.');
    }

    const currentState = await this.meetingStateRepository.findOne({
      where: { meetingStateId: meeting.meetingStateId },
    });

    if (currentState?.description?.toLowerCase() !== 'borrador') {
      throw new BadRequestException('Solo se pueden descartar reuniones en borrador.');
    }

    const discardedState = await this.meetingStateRepository.findOne({
      where: { description: 'Descartada' },
    });

    if (!discardedState) {
      throw new BadRequestException('Estado Descartada no configurado.');
    }

    meeting.meetingStateId = discardedState.meetingStateId;

    return this.meetingRepository.save(meeting);
  }

  async cancelMeeting(identificationId: string, meetingId: number) {
    const pdu = await this.pduRepository.findOne({
      where: { identificationId },
    });

    if (!pdu) {
      throw new BadRequestException('El usuario no tiene proveedor asociado.');
    }

    const meeting = await this.meetingRepository.findOne({
      where: {
        id: meetingId,
        providerId: pdu.providerId,
      },
    });

    if (!meeting) {
      throw new BadRequestException('La reunión no existe o no pertenece al proveedor.');
    }

    const currentState = await this.meetingStateRepository.findOne({
      where: { meetingStateId: meeting.meetingStateId },
    });

    if (currentState?.description?.toLowerCase() !== 'pendiente') {
      throw new BadRequestException('Solo se pueden cancelar reuniones pendientes.');
    }

    const canceledState = await this.meetingStateRepository.findOne({
      where: { description: 'Cancelada' },
    });

    if (!canceledState) {
      throw new BadRequestException('Estado Cancelada no configurado.');
    }

    meeting.meetingStateId = canceledState.meetingStateId;

    return this.meetingRepository.save(meeting);
  }
}
