import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_TIPOS_DOCUMENTOS')
export class DocumentTypeEntity {
    @PrimaryColumn({ name: 'ID_TIPO_DOCUMENTOS' })
    id!: number;

    @Column({ name: 'DESCRIPCION_TIPO_DOCUMENTOS', nullable: true })
    description!: string;
}