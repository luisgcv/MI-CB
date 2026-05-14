import { IsNotEmpty } from 'class-validator';

export class LoginDto {
  @IsNotEmpty()
  idIdentificacion!: string;

  @IsNotEmpty()
  password!: string;
}
