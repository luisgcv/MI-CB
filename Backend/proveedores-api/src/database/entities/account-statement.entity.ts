import { Column, Entity, JoinColumn, ManyToOne, OneToMany, PrimaryColumn } from 'typeorm';
import { ProviderEntity } from './provider.entity';
import { DocumentTypeEntity } from './account-statement-type.entity';
import { DocumentStatusEntity } from './account-statement-status.entity';
import { DocumentLineEntity } from './account-statement-line.entity';

@Entity('TBL_PROV_DOCUMENTOS')
export class DocumentEntity {
    @PrimaryColumn({ name: 'DOCUMENTO_CONSECUTIVO' })
    id!: string;

    @Column({ name: 'ID_PROVEEDOR', nullable: true })
    providerId!: string;

    @Column({ name: 'ID_TIPO_DOCUMENTOS' })
    typeId!: number;

    @Column({ name: 'ID_ESTADOS_DOCUMENTOS' })
    statusId!: number;

    @Column({ name: 'FECHA_DOCUMENTO' })
    documentDate!: Date;

    @Column({ name: 'FECHA_PAGO' })
    paymentDate!: Date;

    @Column({ name: 'MONTO', type: 'decimal', nullable: true, default: 0 })
    amount!: number;

    @ManyToOne(() => ProviderEntity)
    @JoinColumn({ name: 'ID_PROVEEDOR' })
    provider!: ProviderEntity;

    @ManyToOne(() => DocumentTypeEntity)
    @JoinColumn({ name: 'ID_TIPO_DOCUMENTOS' })
    type!: DocumentTypeEntity;

    @ManyToOne(() => DocumentStatusEntity)
    @JoinColumn({ name: 'ID_ESTADOS_DOCUMENTOS' })
    status!: DocumentStatusEntity;

    @OneToMany(() => DocumentLineEntity, (line) => line.document)
    lines!: DocumentLineEntity[];
}