import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';
import { ContractEntity } from './contract.entity';

@Entity('TBL_PROV_CONTRATOS_LINEAS')
export class ContractLineEntity {
  @PrimaryGeneratedColumn({ name: 'ID_LINEA' })
  id!: number;

  @Column({ name: 'CONTATOS_CONSECUTIVO' })
  consecutivo!: string;

  @Column({ name: 'LINEA_DESCRIPCION' })
  descripcion!: string;

  @Column({ name: 'CANTIDAD', type: 'decimal', nullable: true, default: 0 })
  cantidad!: number;

  @Column({ name: 'MONTO', type: 'decimal', nullable: true, default: 0 })
  monto!: number;

  @ManyToOne(() => ContractEntity, (c) => c.lineas)
  @JoinColumn({ name: 'CONTATOS_CONSECUTIVO' })
  contract!: ContractEntity;
}
