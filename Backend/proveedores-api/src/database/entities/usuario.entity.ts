import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_USUARIOS')
export class UsuarioEntity {
  @PrimaryColumn({
    name: 'ID_IDENTIFICACION',
  })
  idIdentificacion!: string;

  @Column({
    name: 'CONTRASEÑA',
  })
  password!: string;

  @Column({
    name: 'IP_ULTIMO_ACCESO',
    nullable: true,
  })
  ipUltimoAcceso!: string;

  @Column({
    name: 'ULTIMO_INICIO_SESION',
    nullable: true,
  })
  ultimoInicioSesion!: Date;

  @Column({
    name: 'ULTIMO_CAMBIO_CONTRASEÑA',
    nullable: true,
  })
  ultimoCambioContrasena!: Date;
}
