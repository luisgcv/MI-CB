import {
  ArrayNotEmpty,
  IsArray,
  IsBoolean,
  IsNotEmpty,
  IsNumber,
  IsOptional,
  IsString,
  ValidateIf,
} from 'class-validator';
import { Transform, Type } from 'class-transformer';

function toNumberArray(value: unknown): number[] {
  if (Array.isArray(value)) {
    return value
      .map((item) => Number(item))
      .filter((item) => Number.isFinite(item));
  }

  if (typeof value === 'string') {
    const trimmed = value.trim();
    if (!trimmed) {
      return [];
    }

    try {
      const parsed = JSON.parse(trimmed);
      if (Array.isArray(parsed)) {
        return parsed
          .map((item) => Number(item))
          .filter((item) => Number.isFinite(item));
      }
    } catch {
      return trimmed
        .split(',')
        .map((item) => Number(item.trim()))
        .filter((item) => Number.isFinite(item));
    }
  }

  return [];
}

function toBoolean(value: unknown): boolean {
  if (typeof value === 'boolean') {
    return value;
  }

  if (typeof value === 'number') {
    return value !== 0;
  }

  if (typeof value === 'string') {
    const normalized = value.trim().toLowerCase();
    return ['true', '1', 'si', 'sí', 's', 'on', 'yes'].includes(normalized);
  }

  return false;
}

export class CreateProductDto {
  @IsNotEmpty()
  @IsString()
  idSku!: string;

  @IsOptional()
  @IsString()
  descripcion?: string;

  @Type(() => Number)
  @IsNumber()
  idUnidadMedida!: number;

  @Type(() => Number)
  @IsNumber()
  idCondicionesDevolucion!: number;

  @Type(() => Number)
  @IsNumber()
  @IsOptional()
  porcIva?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  minDespacho?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  embalaje?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  gramaje?: number;

  @IsOptional()
  @Transform(({ value }) => toBoolean(value))
  @IsBoolean()
  devuelveMuestras?: boolean;

  @IsOptional()
  @Transform(({ value }) => toBoolean(value))
  @IsBoolean()
  deseaPublicidad?: boolean;

  @IsOptional()
  @Transform(({ value }) => toBoolean(value))
  @IsBoolean()
  aceptaDevolucion?: boolean;

  @IsOptional()
  @Transform(({ value }) => toBoolean(value))
  @IsBoolean()
  tienePoliticaCambios?: boolean;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  condicionPago?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  costoSinIva?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  costoConIva?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  descuentoIntroduccion?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  descuentoEspecial?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  descuentoPermanente?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  margenSugerido?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  alto?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  ancho?: number;

  @IsOptional()
  @Type(() => Number)
  @IsNumber()
  profundidad?: number;

  @Transform(({ value }) => toNumberArray(value))
  @IsArray()
  @ArrayNotEmpty()
  idSucursales!: number[];

  @ValidateIf((o) => {
    const forma = (o.formaFarmaceutica ?? '') as string;
    const registro = (o.registroMedicamento ?? '') as string;
    return Boolean((forma && forma.toString().trim()) || (registro && registro.toString().trim()));
  })
  @IsNotEmpty()
  @IsString()
  @IsOptional()
  idCabys?: string;

  @IsOptional()
  @IsString()
  formaFarmaceutica?: string;

  @IsOptional()
  @IsString()
  registroMedicamento?: string;
}