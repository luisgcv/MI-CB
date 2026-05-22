import { IsBoolean, IsNotEmpty, IsNumber, IsOptional, IsString } from 'class-validator';

export class CreateMeetingDto {
  @IsNotEmpty()
  @IsString()
  motivo!: string;

  @IsNotEmpty()
  @IsString()
  tipoReunion!: string;

  @IsNotEmpty()
  @IsNumber()
  departmentId!: number;

  @IsString()
  @IsOptional()
  observaciones?: string;

  @IsNotEmpty()
  @IsString()
  fechaHoraInicio!: string;

  @IsNotEmpty()
  @IsString()
  fechaHoraHasta!: string;

  @IsOptional()
  @IsBoolean()
  isDraft?: boolean;
}
