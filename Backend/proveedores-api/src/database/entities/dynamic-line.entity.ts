import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';

import { DynamicEntity } from './dynamic.entity';

@Entity('TBL_PROV_DINAMICAS_LINEAS')
export class DynamicLineEntity {
  @PrimaryGeneratedColumn({
    name: 'ID_LINEA',
  })
  id!: number;

  @Column({
    name: 'DINAMICAS_CONSECUTIVO',
  })
  dynamicId!: string;

  @Column({
    name: 'LINEA_ARTICULO',
  })
  sku!: string;

  @Column({
    name: 'LINEA_DESCRIPCION',
  })
  description!: string;

  @Column({
    name: 'CANTIDAD',
    type: 'decimal',
  })
  quantity!: number;

  @Column({
    name: 'MONTO',
    type: 'decimal',
  })
  amount!: number;

  @ManyToOne(
    () => DynamicEntity,
    (dynamic) => dynamic.lines,
  )
  @JoinColumn({
    name: 'DINAMICAS_CONSECUTIVO',
  })
  dynamic!: DynamicEntity;
}