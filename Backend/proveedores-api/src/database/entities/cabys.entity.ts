import { Column, Entity, PrimaryColumn } from 'typeorm';

@Entity('TBL_PROV_CABYS')
export class CabysEntity {
  @PrimaryColumn({ name: 'ID_CABYS' })
  cabysId!: string;

  @Column({ name: 'NOMBRE_CABYS' })
  cabysName!: string;
}