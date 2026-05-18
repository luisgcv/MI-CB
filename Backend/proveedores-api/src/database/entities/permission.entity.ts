import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_PERMISOS')
export class PermissionEntity {
  @PrimaryColumn({ name: 'ID_PERMISO' }) permissionId!: number;
  @Column({ name: 'NOMBRE_PERMISO' }) permissionName!: string;
}
