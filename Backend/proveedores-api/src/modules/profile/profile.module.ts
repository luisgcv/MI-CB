import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { ProfileController } from './profile.controller';
import { ProfileService } from './profile.service';
import { UserInformationEntity } from '../../database/entities/user-information.entity';
import { ProviderUserDepartmentEntity } from '../../database/entities/provider-user-department.entity';
import { ProviderEntity } from '../../database/entities/provider.entity';
import { DepartmentEntity } from '../../database/entities/department.entity';

@Module({
    imports: [
        TypeOrmModule.forFeature([
            UserInformationEntity,
            ProviderUserDepartmentEntity,
            ProviderEntity,
            DepartmentEntity,
        ]),
    ],
    controllers: [ProfileController],
    providers: [ProfileService],
})
export class ProfileModule { }