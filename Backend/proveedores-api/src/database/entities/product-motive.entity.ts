import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_PRODUCTOS_MOTIVOS')
export class ProductMotiveEntity {
  @PrimaryColumn({ name: 'ID_MOTIVO' })
  motiveId!: number;

  @Column({ name: 'DESCRIPCION_MOTIVO', type: 'varchar', length: 300, nullable: true })
  motiveDescription!: string | null;
}