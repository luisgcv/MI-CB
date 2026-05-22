import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_REUNIONES_TIPOS')
export class MeetingTypeEntity {
  @PrimaryColumn({ name: 'ID_TIPO_REUNION', type: 'int' })
  meetingTypeId!: number;

  @Column({ name: 'DESCRIPCION_TIPO_REUNION', length: 300, nullable: true })
  description!: string;
}
