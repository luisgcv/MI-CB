import { Entity, JoinColumn, ManyToOne, PrimaryColumn } from 'typeorm';
import { PermissionEntity } from './permission.entity';
import { UserEntity } from './user.entity';
@Entity('TBL_PROV_PERFILES')
export class ProfileEntity {
  @PrimaryColumn({ name: 'ID_PERMISO' }) permissionId!: number;
  @PrimaryColumn({ name: 'ID_IDENTIFICACION' }) identificationId!: string;
  @ManyToOne(() => PermissionEntity)
  @JoinColumn({ name: 'ID_PERMISO' })
  permission!: PermissionEntity;
  @ManyToOne(() => UserEntity)
  @JoinColumn({ name: 'ID_IDENTIFICACION' })
  user!: UserEntity;
}
