import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  OneToMany,
  PrimaryColumn,
} from 'typeorm';
import { ProviderEntity } from './provider.entity';
import { BranchEntity } from './branch.entity';
import { ContractLineEntity } from './contract-line.entity';
import { ContractChargeEntity } from './contract-charge.entity';

@Entity('TBL_PROV_CONTRATOS')
export class ContractEntity {
  @PrimaryColumn({ name: 'CONTATOS_CONSECUTIVO' })
  consecutivo!: string;

  @Column({ name: 'TIPO_CONTRATO' })
  tipoContrato!: string;

  @Column({ name: 'ID_SUCURSAL' })
  idSucursal!: number;

  @Column({ name: 'ID_PROVEEDOR' })
  idProveedor!: string;

  @Column({ name: 'FECHA_INICIO' })
  fechaInicio!: Date;

  @Column({ name: 'FECHA_HASTA' })
  fechaHasta!: Date;

  @Column({
    name: 'MONTO_MENSUAL',
    type: 'decimal',
    nullable: true,
    default: 0,
  })
  montoMensual!: number;

  @ManyToOne(() => ProviderEntity)
  @JoinColumn({ name: 'ID_PROVEEDOR' })
  proveedor!: ProviderEntity;

  @ManyToOne(() => BranchEntity)
  @JoinColumn({ name: 'ID_SUCURSAL' })
  sucursal!: BranchEntity;

  @OneToMany(() => ContractLineEntity, (line) => line.contract)
  lineas!: ContractLineEntity[];

  @OneToMany(() => ContractChargeEntity, (charge) => charge.contract)
  cobros!: ContractChargeEntity[];
}
