import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { UserInformationEntity } from '../../database/entities/user-information.entity';
import { ProviderUserDepartmentEntity } from '../../database/entities/provider-user-department.entity';
import { ProviderEntity } from '../../database/entities/provider.entity';
import { DepartmentEntity } from '../../database/entities/department.entity';

@Injectable()
export class ProfileService {
    constructor(
        @InjectRepository(UserInformationEntity)
        private userInfoRepo: Repository<UserInformationEntity>,

        @InjectRepository(ProviderUserDepartmentEntity)
        private pduRepo: Repository<ProviderUserDepartmentEntity>,

        @InjectRepository(ProviderEntity)
        private providerRepo: Repository<ProviderEntity>,

        @InjectRepository(DepartmentEntity)
        private departmentRepo: Repository<DepartmentEntity>,
    ) { }

    async getProfile(identificationId: string) {
        // Nombre y teléfono
        const info = await this.userInfoRepo.findOne({
            where: { identificationId },
        });

        // Relación usuario-empresa-departamento (primera que encuentre)
        const pdu = await this.pduRepo.findOne({
            where: { identificationId },
        });

        // Empresa
        const provider = pdu
            ? await this.providerRepo.findOne({ where: { providerId: pdu.providerId } })
            : null;

        // Departamento
        const department = pdu
            ? await this.departmentRepo.findOne({ where: { departmentId: pdu.departmentId } })
            : null;

        return {
            identificationNumber: identificationId,
            name: info?.name ?? null,
            phone: info?.phone ?? null,
            email: pdu?.email ?? null,
            company: provider?.providerName ?? null,
            department: department?.departmentName ?? null,
            position: pdu?.position ?? null,
        };
    }
}