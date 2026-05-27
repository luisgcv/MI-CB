import { Column, Entity, JoinColumn, ManyToOne, PrimaryColumn } from 'typeorm';
import { ProductEntity } from './product.entity';

@Entity('TBL_PROV_PRODUCTOS_IMAGENES')
export class ProductImageEntity {
  @PrimaryColumn({ name: 'ID_SKU' })
  sku!: string;

  @PrimaryColumn({ name: 'NOMBRE_ARCHIVO' })
  fileName!: string;

  @Column({ name: 'ARCHIVO' })
  filePath!: string;

  @ManyToOne(() => ProductEntity, (product) => product.images)
  @JoinColumn({ name: 'ID_SKU' })
  product!: ProductEntity;
}