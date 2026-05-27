import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_ESTADOS_PRODUCTOS')
export class ProductStatusEntity {
  @PrimaryColumn({ name: 'ID_ESTADO' })
  statusId!: number;

  @Column({ name: 'DESCRIPCION_ESTADO' })
  statusDescription!: string;
}