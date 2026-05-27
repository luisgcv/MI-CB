import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_CONDICIONES_DEVOLUCION')
export class ReturnConditionEntity {
  @PrimaryColumn({ name: 'ID_CONDICIONES_DEVOLUCION' })
  returnConditionId!: number;

  @Column({ name: 'NOMBRE_CONDICIONES_DEVOLUCION' })
  returnConditionName!: string;
}