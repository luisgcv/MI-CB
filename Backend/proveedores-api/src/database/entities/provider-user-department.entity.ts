import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_PROVEEDORES_DEPARTAMENTOS_USUARIO')
export class ProviderUserDepartmentEntity {
  @PrimaryColumn({
    name: 'ID_PROVEEDOR',
  })
  providerId!: string;

  @PrimaryColumn({
    name: 'ID_IDENTIFICACION',
  })
  identificationId!: string;

  @PrimaryColumn({
    name: 'ID_DEPARTAMENTO',
  })
  departmentId!: number;

  @Column({
    name: 'PUESTO',
    nullable: true,
  })
  position!: string;

  @Column({
    name: 'CORREO_NOTIFICACION',
    nullable: true,
  })
  email!: string;
}
