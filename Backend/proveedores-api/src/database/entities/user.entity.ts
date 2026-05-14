import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_USUARIOS')
export class UserEntity {
  @PrimaryColumn({
    name: 'ID_IDENTIFICACION',
  })
  identificationId!: string;

  @Column({
    name: 'CONTRASEÑA',
  })
  password!: string;

  @Column({
    name: 'IP_ULTIMO_ACCESO',
    nullable: true,
  })
  lastAccessIp!: string;

  @Column({
    name: 'ULTIMO_INICIO_SESION',
    nullable: true,
  })
  lastLogin!: Date;

  @Column({
    name: 'ULTIMO_CAMBIO_CONTRASEÑA',
    nullable: true,
  })
  lastPasswordChange!: Date;
}
