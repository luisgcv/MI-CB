import { Controller, Get, Req, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { ProfileService } from './profile.service';

@Controller('profile')
@UseGuards(JwtAuthGuard)
export class ProfileController {
    constructor(private readonly profileService: ProfileService) { }

    @Get()
    getProfile(@Req() req: any) {
        // El JWT guard pone el usuario en req.user
        // En auth.service.ts el payload fue: { sub: user.identificationId }
        const identificationId: string = req.user.sub;
        return this.profileService.getProfile(identificationId);
    }
}