import { Column, Entity, JoinColumn, ManyToOne, PrimaryColumn } from 'typeorm';
import { PurchaseOrderEntity } from './purchase-order.entity';

@Entity('TBL_PROV_ORDENES_LINEAS')
export class PurchaseOrderLineEntity {
    @PrimaryColumn({ name: 'ID_ORDEN' })
    orderId!: string;

    @PrimaryColumn({ name: 'SKU_ORDEN' })
    sku!: string;

    @Column({ name: 'SKU_ORDEN_DESCRIPCION' })
    description!: string;

    @Column({ name: 'COSTO', type: 'decimal', nullable: true, default: 0 })
    cost!: number;

    @Column({ name: 'CANTIDAD_ORDENADA', type: 'decimal', nullable: true, default: 0 })
    quantityOrdered!: number;

    @Column({ name: 'CANTIDAD_RECIBIDA', type: 'decimal', nullable: true, default: 0 })
    quantityReceived!: number;

    @Column({ name: 'CANTIDAD_DEVOLUCION', type: 'decimal', nullable: true, default: 0 })
    quantityReturned!: number;

    @Column({ name: 'CANTIDAD_BACKORDER', type: 'decimal', nullable: true, default: 0 })
    quantityBackorder!: number;

    @ManyToOne(() => PurchaseOrderEntity, (order) => order.lines)
    @JoinColumn({ name: 'ID_ORDEN' })
    order!: PurchaseOrderEntity;
}