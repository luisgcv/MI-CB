import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  PrimaryGeneratedColumn,
} from 'typeorm';
import { ContractEntity } from './contract.entity';

@Entity('TBL_PROV_CONTRATOS_COBROS')
export class ContractChargeEntity {
  @PrimaryGeneratedColumn({ name: 'ID_COBRO' })
  id!: number;

  @Column({ name: 'CONTATOS_CONSECUTIVO' })
  consecutivo!: string;

  @Column({ name: 'MONTO', type: 'decimal', nullable: true, default: 0 })
  monto!: number;

  @Column({ name: 'DOCUMENTO' })
  documento!: string;

  @Column({ name: 'FECHA' })
  fecha!: Date;

  @ManyToOne(() => ContractEntity, (c) => c.cobros)
  @JoinColumn({ name: 'CONTATOS_CONSECUTIVO' })
  contract!: ContractEntity;
}
