import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';

import { DynamicEntity } from './dynamic.entity';

@Entity('TBL_PROV_DINAMICAS_COBROS')
export class DynamicChargeEntity {
  @PrimaryGeneratedColumn({
    name: 'ID_COBRO',
  })
  id!: number;

  @Column({
    name: 'DINAMICAS_CONSECUTIVO',
  })
  dynamicId!: string;

  @Column({
    name: 'MONTO',
    type: 'decimal',
  })
  amount!: number;

  @Column({
    name: 'DOCUMENTO',
  })
  document!: string;

  @Column({
    name: 'FECHA',
  })
  date!: Date;

  @ManyToOne(
    () => DynamicEntity,
    (dynamic) => dynamic.charges,
  )
  @JoinColumn({
    name: 'DINAMICAS_CONSECUTIVO',
  })
  dynamic!: DynamicEntity;
}