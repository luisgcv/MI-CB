import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { JwtModule } from '@nestjs/jwt';

import { AuthService } from './auth.service';
import { AuthController } from './auth.controller';

import { UserEntity } from '../../database/entities/user.entity';
import { SessionLogEntity } from '../../database/entities/session-log.entity';
import { ProviderUserDepartmentEntity } from '../../database/entities/provider-user-department.entity';

import { PassportModule } from '@nestjs/passport';
import { JwtStrategy } from './jwt.strategy';

@Module({
  imports: [
    TypeOrmModule.forFeature([
      UserEntity,
      SessionLogEntity,
      ProviderUserDepartmentEntity,
    ]),
    PassportModule,
    JwtModule.register({
      secret: process.env.JWT_SECRET,

      signOptions: {
        expiresIn: '1d',
      },
    }),
  ],

  providers: [AuthService, JwtStrategy],

  controllers: [AuthController],
})
export class AuthModule {}
