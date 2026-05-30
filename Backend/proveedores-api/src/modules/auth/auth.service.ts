import { Injectable, UnauthorizedException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { JwtService } from '@nestjs/jwt';

import { UserEntity } from '../../database/entities/user.entity';
import { SessionLogEntity } from '../../database/entities/session-log.entity';

import { ProviderUserDepartmentEntity } from '../../database/entities/provider-user-department.entity';
//ProviderUserDepartment = PDU

@Injectable()
export class AuthService {
  constructor(
    @InjectRepository(UserEntity)
    private userRepository: Repository<UserEntity>,

    @InjectRepository(SessionLogEntity)
    private sessionLogRepository: Repository<SessionLogEntity>,

    @InjectRepository(ProviderUserDepartmentEntity)
      private pduRepository: Repository<ProviderUserDepartmentEntity>,

    private jwtService: JwtService,
  ) {}

  async login(identificationId: string, password: string, ip: string) {
    const user = await this.userRepository.findOne({
      where: {
        identificationId,
      },
    });

    if (!user) {
      throw new UnauthorizedException('Usuario no existe');
    }

    if (user.password !== password) {
      throw new UnauthorizedException('Contraseña incorrecta');
    }

    user.lastLogin = new Date();
    user.lastAccessIp = ip;

    await Promise.all([
      this.userRepository.save(user),
      this.sessionLogRepository.save(
        this.sessionLogRepository.create({
          identificationId,
          loginDate: new Date(),
        }),
      ),
    ]);

    const pdu = await this.pduRepository.findOne({
        where: {
            identificationId
        },
    });

    const payload = {
        sub: user.identificationId,
        providerId: pdu?.providerId ?? null,
    };

    return {
      token: this.jwtService.sign(payload),
    };
  }
}
