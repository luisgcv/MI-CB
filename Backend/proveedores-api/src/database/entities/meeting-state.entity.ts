import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_REUNIONES_ESTADOS')
export class MeetingStateEntity {
  @PrimaryColumn({ name: 'ID_ESTADO_REUNION', type: 'int' })
  meetingStateId!: number;

  @Column({ name: 'DESCRIPCION_ESTADO_REUNION', length: 300, nullable: true })
  description!: string;
}
