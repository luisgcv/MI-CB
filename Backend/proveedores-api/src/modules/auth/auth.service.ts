import { Injectable, UnauthorizedException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { JwtService } from '@nestjs/jwt';

import { UserEntity } from '../../database/entities/user.entity';

@Injectable()
export class AuthService {
  constructor(
    @InjectRepository(UserEntity)
    private userRepository: Repository<UserEntity>,

    private jwtService: JwtService,
  ) {}

  async login(identificationId: string, password: string) {
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

    await this.userRepository.save(user);

    const payload = {
      sub: user.identificationId,
    };

    return {
      token: this.jwtService.sign(payload),
    };
  }
}
