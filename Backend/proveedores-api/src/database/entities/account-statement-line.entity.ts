import { Column, Entity, JoinColumn, ManyToOne, PrimaryGeneratedColumn } from 'typeorm';
import { DocumentEntity } from './account-statement.entity';

@Entity('TBL_PROV_DOCUMENTOS_LINEAS')
export class DocumentLineEntity {
    @PrimaryGeneratedColumn({ name: 'ID_LINEA' })
    id!: number;

    @Column({ name: 'DOCUMENTO_CONSECUTIVO' })
    documentId!: string;

    @Column({ name: 'DESCRIPCION' })
    description!: string;

    @Column({ name: 'MONTO', type: 'decimal', nullable: true, default: 0 })
    amount!: number;

    @ManyToOne(() => DocumentEntity, (doc) => doc.lines)
    @JoinColumn({ name: 'DOCUMENTO_CONSECUTIVO' })
    document!: DocumentEntity;
}