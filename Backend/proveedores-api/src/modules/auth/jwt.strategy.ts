import { Injectable } from '@nestjs/common';
import { PassportStrategy } from '@nestjs/passport';
import { InjectRepository } from '@nestjs/typeorm';
import { ExtractJwt, Strategy } from 'passport-jwt';
import { Repository } from 'typeorm';
import { ProviderUserDepartmentEntity } from '../../database/entities/provider-user-department.entity';

@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy) {
  constructor(
    @InjectRepository(ProviderUserDepartmentEntity)
    private providerUserRepository: Repository<ProviderUserDepartmentEntity>,
  ) {
    super({
      jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
      ignoreExpiration: false,
      secretOrKey: process.env.JWT_SECRET ?? 'fallback_secret',
    });
  }

  async validate(payload: any) {
    const providerUser = await this.providerUserRepository.findOne({
      where: { identificationId: payload.sub },
    });

    return {
      userId: payload.sub,
      providerId: providerUser?.providerId ?? null,
    };
  }
}
