import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_PROVEEDORES')
export class ProviderEntity {
  @PrimaryColumn({ name: 'ID_PROVEEDOR' }) providerId!: string;
  @Column({ name: 'NOMBRE_PROVEEDOR' }) providerName!: string;
}
