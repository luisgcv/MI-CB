import { Entity, JoinColumn, ManyToOne, PrimaryColumn } from 'typeorm';
import { ProductEntity } from './product.entity';
import { BranchEntity } from './branch.entity';

@Entity('TBL_PROV_PRODUCTOS_SUCURSALES')
export class ProductBranchEntity {
  @PrimaryColumn({ name: 'ID_SKU' })
  sku!: string;

  @PrimaryColumn({ name: 'ID_SUCURSAL' })
  branchId!: number;

  @ManyToOne(() => ProductEntity, (product) => product.branches)
  @JoinColumn({ name: 'ID_SKU' })
  product!: ProductEntity;

  @ManyToOne(() => BranchEntity)
  @JoinColumn({ name: 'ID_SUCURSAL' })
  branch!: BranchEntity;
}