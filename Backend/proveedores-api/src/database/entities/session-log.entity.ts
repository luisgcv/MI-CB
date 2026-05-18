import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';
import { UserEntity } from './user.entity';

@Entity('TBL_PROV_BITACORA_SESION')
export class SessionLogEntity {
  @PrimaryGeneratedColumn({ name: 'ID_BITACORA' }) id!: number;
  @Column({ name: 'ID_IDENTIFICACION' }) identificationId!: string;
  @Column({ name: 'FECHA_INICIO_SESION', nullable: true }) loginDate!: Date;
  @ManyToOne(() => UserEntity)
  @JoinColumn({ name: 'ID_IDENTIFICACION' })
  user!: UserEntity;
}
