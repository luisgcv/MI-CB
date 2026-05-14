import { IsNotEmpty } from 'class-validator';

export class LoginDto {
  @IsNotEmpty()
  identificationId!: string;

  @IsNotEmpty()
  password!: string;
}
