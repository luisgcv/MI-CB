import { Column, Entity, JoinColumn, ManyToOne, OneToMany, PrimaryColumn } from 'typeorm';
import { ProviderEntity } from './provider.entity';
import { BranchEntity } from './branch.entity';
import { PurchaseOrderLineEntity } from './purchase-order-line.entity';

@Entity('TBL_PROV_ORDENES_ENCABEZADO')
export class PurchaseOrderEntity {
    @PrimaryColumn({ name: 'ID_ORDEN' })
    id!: string;

    @Column({ name: 'ID_SUCURSAL' })
    branchId!: number;

    @Column({ name: 'ID_PROVEEDOR', nullable: true })
    providerId!: string;

    @Column({ name: 'ID_ESTADO_ORDEN' })
    statusId!: number;

    @Column({ name: 'FECHA_CREACION' })
    createdAt!: Date;

    @Column({ name: 'FECHA_REQUERIDA', nullable: true })
    requiredAt!: Date;

    @Column({ name: 'NOMBRE_CREADOR' })
    createdBy!: string;

    @Column({ name: 'MONTO_TOTAL', type: 'decimal' })
    total!: number;

    @ManyToOne(() => ProviderEntity)
    @JoinColumn({ name: 'ID_PROVEEDOR' })
    provider!: ProviderEntity;

    @ManyToOne(() => BranchEntity)
    @JoinColumn({ name: 'ID_SUCURSAL' })
    branch!: BranchEntity;

    @OneToMany(() => PurchaseOrderLineEntity, (line) => line.order)
    lines!: PurchaseOrderLineEntity[];
}