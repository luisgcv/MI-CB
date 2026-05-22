import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';

@Entity('TBL_PROV_REUNIONES')
export class MeetingEntity {
  @PrimaryGeneratedColumn({ type: 'int', name: 'ID' })
  id!: number;

  @Column({ name: 'ID_PROVEEDOR', length: 20 })
  providerId!: string;

  @Column({ name: 'ID_TIPO_REUNION', type: 'int' })
  meetingTypeId!: number;

  @Column({ name: 'ID_DEPARTAMENTO', type: 'int' })
  departmentId!: number;

  @Column({ name: 'ID_ESTADO_REUNION', type: 'int' })
  meetingStateId!: number;

  @Column({ name: 'MOTIVO', length: 200 })
  reason!: string;

  @Column({ name: 'OBERVACIONES', length: 200 })
  observations!: string;

  @Column({ name: 'FECHA_HORA_INICIO', type: 'datetime' })
  startDateTime!: Date;

  @Column({ name: 'FECHA_HORA_HASTA', type: 'datetime' })
  endDateTime!: Date;
}
