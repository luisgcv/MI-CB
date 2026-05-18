import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_DEPARTAMENTOS')
export class DepartmentEntity {
  @PrimaryColumn({ name: 'ID_DEPARTAMENTO' }) departmentId!: number;
  @Column({ name: 'NOMBRE_DEPARTAMENTO' }) departmentName!: string;
  @Column({ name: 'CORREOS_NOTIFICACION_REUNION' })
  meetingNotificationEmails!: string;
}
