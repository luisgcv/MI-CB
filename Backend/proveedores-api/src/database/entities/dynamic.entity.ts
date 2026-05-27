import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  OneToMany,
  PrimaryColumn,
} from 'typeorm';

import { BranchEntity } from './branch.entity';
import { DynamicLineEntity } from './dynamic-line.entity';
import { DynamicChargeEntity } from './dynamic-charge.entity';

@Entity('TBL_PROV_DINAMICAS')
export class DynamicEntity {
  @PrimaryColumn({
    name: 'DINAMICAS_CONSECUTIVO',
  })
  id!: string;

  @Column({
    name: 'TIPO_DINAMICA',
  })
  tipoDinamica!: string;

  @Column({
    name: 'ID_SUCURSAL',
  })
  branchId!: number;

  @Column({
    name: 'ID_PROVEEDOR',
  })
  providerId!: string;

  @Column({
    name: 'FECHA_INICIO',
  })
  startDate!: Date;

  @Column({
    name: 'FECHA_HASTA',
  })
  endDate!: Date;

  @Column({
    name: 'MONTO_TOTAL',
    type: 'decimal',
  })
  totalAmount!: number;

  @ManyToOne(() => BranchEntity)
  @JoinColumn({
    name: 'ID_SUCURSAL',
  })
  branch!: BranchEntity;

  @OneToMany(
    () => DynamicLineEntity,
    (line) => line.dynamic,
  )
  lines!: DynamicLineEntity[];

  @OneToMany(
    () => DynamicChargeEntity,
    (charge) => charge.dynamic,
  )
  charges!: DynamicChargeEntity[];
}