import { Column, Entity, JoinColumn, ManyToOne, PrimaryGeneratedColumn } from 'typeorm';
import { ProductEntity } from './product.entity';
import { ProductStatusEntity } from './product-status.entity';
import { ProductMotiveEntity } from './product-motive.entity';

@Entity('TBL_PROV_PRODUCTOS_ESTADOS')
export class ProductStateHistoryEntity {
  @PrimaryGeneratedColumn({ name: 'ID_PRODUCTO_ESTADO' })
  productStateId!: number;

  @Column({ name: 'ID_SKU', type: 'varchar', length: 15 })
  sku!: string;

  @Column({ name: 'ID_ESTADO' })
  statusId!: number;

  @Column({ name: 'ID_MOTIVO' })
  motiveId!: number;

  @Column({ name: 'COMENTARIO', type: 'varchar', length: 300, nullable: true })
  comment!: string | null;

  @Column({ name: 'FECHA', type: 'datetime', nullable: true, default: () => 'GETDATE()' })
  createdAt!: Date | null;

  @ManyToOne(() => ProductEntity)
  @JoinColumn({ name: 'ID_SKU' })
  product!: ProductEntity;

  @ManyToOne(() => ProductStatusEntity)
  @JoinColumn({ name: 'ID_ESTADO' })
  status!: ProductStatusEntity;

  @ManyToOne(() => ProductMotiveEntity)
  @JoinColumn({ name: 'ID_MOTIVO' })
  motive!: ProductMotiveEntity;
}