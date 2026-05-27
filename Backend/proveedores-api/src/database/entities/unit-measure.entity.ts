import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_UNIDADES_MEDIDA')
export class UnitMeasureEntity {
  @PrimaryColumn({ name: 'ID_UNIDAD_MEDIDA' })
  unitMeasureId!: number;

  @Column({ name: 'NOMBRE_UNIDAD_MEDIDA' })
  unitMeasureName!: string;
}