import { Injectable, UnauthorizedException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { JwtService } from '@nestjs/jwt';
import { UsuarioEntity } from '../../database/entities/usuario.entity';

@Injectable()
export class AuthService {
  constructor(
    @InjectRepository(UsuarioEntity)
    private usuarioRepository: Repository<UsuarioEntity>,

    private jwtService: JwtService,
  ) {}

  async login(idIdentificacion: string, password: string) {
    const usuario = await this.usuarioRepository.findOne({
      where: {
        idIdentificacion,
      },
    });

    if (!usuario) {
      throw new UnauthorizedException('Usuario no existe');
    }

    if (usuario.password !== password) {
      throw new UnauthorizedException('Contraseña incorrecta');
    }

    const payload = {
      sub: usuario.idIdentificacion,
    };

    return {
      token: this.jwtService.sign(payload),
    };
  }
}
