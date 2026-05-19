import { Column, Entity, JoinColumn, OneToOne, PrimaryColumn } from 'typeorm';
import { UserEntity } from './user.entity';
@Entity('TBL_PROV_USUARIOS_INFORMACION')
export class UserInformationEntity {
  @PrimaryColumn({ name: 'ID_IDENTIFICACION' }) identificationId!: string;
  @Column({ name: 'NOMBRE', nullable: true }) name!: string;
  @Column({ name: 'TELEFONO', nullable: true }) phone!: string;
  @OneToOne(() => UserEntity)
  @JoinColumn({ name: 'ID_IDENTIFICACION' })
  user!: UserEntity;
}
