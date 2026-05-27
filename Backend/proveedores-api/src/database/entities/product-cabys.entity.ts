import { Column, Entity, JoinColumn, ManyToOne, PrimaryColumn } from 'typeorm';
import { ProductEntity } from './product.entity';
import { CabysEntity } from './cabys.entity';

@Entity('TBL_PROV_PRODUCTOS_CABYS')
export class ProductCabysEntity {
  @PrimaryColumn({ name: 'ID_SKU' })
  sku!: string;

  @PrimaryColumn({ name: 'ID_CABYS' })
  cabysId!: string;

  @Column({ name: 'FORMA_FARMACEUTICA' })
  formPharmaceutical!: string;

  @Column({ name: 'REGISTRO_MEDICAMENTO' })
  medicineRegistry!: string;

  @ManyToOne(() => ProductEntity, (product) => product.cabys)
  @JoinColumn({ name: 'ID_SKU' })
  product!: ProductEntity;

  @ManyToOne(() => CabysEntity)
  @JoinColumn({ name: 'ID_CABYS' })
  cabys!: CabysEntity;
}