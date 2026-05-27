import {
  Column,
  Entity,
  JoinColumn,
  ManyToOne,
  OneToMany,
  PrimaryColumn,
} from 'typeorm';
import { ProviderEntity } from './provider.entity';
import { UnitMeasureEntity } from './unit-measure.entity';
import { ReturnConditionEntity } from './return-condition.entity';
import { ProductStatusEntity } from './product-status.entity';
import { ProductImageEntity } from './product-image.entity';
import { ProductBranchEntity } from './product-branch.entity';
import { ProductCabysEntity } from './product-cabys.entity';

@Entity('TBL_PROV_PRODUCTOS')
export class ProductEntity {
  @PrimaryColumn({ name: 'ID_SKU' })
  sku!: string;

  @Column({ name: 'DESCRIPCION', type: 'varchar', length: 100, nullable: true })
  description!: string | null;

  @Column({ name: 'ID_UNIDAD_MEDIDA' })
  unitMeasureId!: number;

  @Column({ name: 'ID_CONDICIONES_DEVOLUCION' })
  returnConditionId!: number;

  @Column({ name: 'ID_ESTADO' })
  statusId!: number;

  @Column({ name: 'ID_PROVEEDOR', type: 'varchar', length: 20, nullable: true })
  providerId!: string | null;

  @Column({ name: 'PORC_IVA', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  vatPercentage!: number | null;

  @Column({ name: 'MIN_DESPACHO', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  minDispatch!: number | null;

  @Column({ name: 'EMBALAJE', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  packaging!: number | null;

  @Column({ name: 'GRAMAJE', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  gramaje!: number | null;

  @Column({ name: 'DEVUELVE_MUESTRAS', type: 'bit', nullable: true, default: 0 })
  returnsSamples!: boolean | null;

  @Column({ name: 'DESEA_PUBLICIDAD', type: 'bit', nullable: true, default: 0 })
  wantsAdvertising!: boolean | null;

  @Column({ name: 'ACEPTA_DEVOLUCION', type: 'bit', nullable: true, default: 0 })
  acceptsReturn!: boolean | null;

  @Column({ name: 'TIENE_POLITICA_CAMBIOS', type: 'bit', nullable: true, default: 0 })
  hasExchangePolicy!: boolean | null;

  @Column({ name: 'CONDICION_PAGO', type: 'int', nullable: true, default: 0 })
  paymentCondition!: number | null;

  @Column({ name: 'COSTO_SIN_IVA', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  costWithoutVat!: number | null;

  @Column({ name: 'COSTO_CON_IVA', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  costWithVat!: number | null;

  @Column({ name: 'DESCUENTO_INTRODUCCION', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  introDiscount!: number | null;

  @Column({ name: 'DESCUENTO_ESPECIAL', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  specialDiscount!: number | null;

  @Column({ name: 'DESCUENTO_PERMANENTE', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  permanentDiscount!: number | null;

  @Column({ name: 'MARGEN_SUGERIDO', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  suggestedMargin!: number | null;

  @Column({ name: 'ALTO', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  height!: number | null;

  @Column({ name: 'ANCHO', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  width!: number | null;

  @Column({ name: 'PROFUNDIDAD', type: 'decimal', precision: 28, scale: 8, nullable: true, default: 0 })
  depth!: number | null;

  @Column({ name: 'FECHA_CREACION', type: 'datetime', nullable: true, default: () => 'GETDATE()' })
  createdAt!: Date | null;

  @ManyToOne(() => ProviderEntity)
  @JoinColumn({ name: 'ID_PROVEEDOR' })
  provider!: ProviderEntity;

  @ManyToOne(() => UnitMeasureEntity)
  @JoinColumn({ name: 'ID_UNIDAD_MEDIDA' })
  unitMeasure!: UnitMeasureEntity;

  @ManyToOne(() => ReturnConditionEntity)
  @JoinColumn({ name: 'ID_CONDICIONES_DEVOLUCION' })
  returnCondition!: ReturnConditionEntity;

  @ManyToOne(() => ProductStatusEntity)
  @JoinColumn({ name: 'ID_ESTADO' })
  status!: ProductStatusEntity;

  @OneToMany(() => ProductImageEntity, (image) => image.product)
  images!: ProductImageEntity[];

  @OneToMany(() => ProductBranchEntity, (branch) => branch.product)
  branches!: ProductBranchEntity[];

  @OneToMany(() => ProductCabysEntity, (cabys) => cabys.product)
  cabys!: ProductCabysEntity[];
}