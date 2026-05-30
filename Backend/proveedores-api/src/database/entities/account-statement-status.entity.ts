import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_ESTADOS_DOCUMENTOS')
export class DocumentStatusEntity {
    @PrimaryColumn({ name: 'ID_ESTADOS_DOCUMENTOS' })
    id!: number;

    @Column({ name: 'DESCRIPCION_ESTADOS_DOCUMENTOS', nullable: true })
    description!: string;
}