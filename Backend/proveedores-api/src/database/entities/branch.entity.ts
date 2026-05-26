import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_SUCURSALES')
export class BranchEntity {
  @PrimaryColumn({ name: 'ID_SUCURSAL' })
  id!: number;

  @Column({ name: 'NOMBRE_SUCURSAL' })
  nombreSucursal!: string;
}
