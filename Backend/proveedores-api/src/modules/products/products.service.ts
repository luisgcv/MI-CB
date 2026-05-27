import {
  BadRequestException,
  Injectable,
  UnauthorizedException,
} from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { In, Repository } from 'typeorm';
import { mkdir, writeFile } from 'fs/promises';
import { join } from 'path';
import { CreateProductDto } from './dto/create-product.dto';
import { UnitMeasureEntity } from '../../database/entities/unit-measure.entity';
import { ReturnConditionEntity } from '../../database/entities/return-condition.entity';
import { BranchEntity } from '../../database/entities/branch.entity';
import { CabysEntity } from '../../database/entities/cabys.entity';
import { ProductEntity } from '../../database/entities/product.entity';
import { ProductImageEntity } from '../../database/entities/product-image.entity';
import { ProductBranchEntity } from '../../database/entities/product-branch.entity';
import { ProductCabysEntity } from '../../database/entities/product-cabys.entity';
import { ProductMotiveEntity } from '../../database/entities/product-motive.entity';
import { ProductStateHistoryEntity } from '../../database/entities/product-state-history.entity';
import { ProviderEntity } from '../../database/entities/provider.entity';
import { ProductStatusEntity } from '../../database/entities/product-status.entity';

type UploadedProductImage = {
  originalname: string;
  buffer: Buffer;
};

@Injectable()
export class ProductsService {
  constructor(
    @InjectRepository(ProductEntity)
    private readonly productRepository: Repository<ProductEntity>,
    @InjectRepository(ProductImageEntity)
    private readonly productImageRepository: Repository<ProductImageEntity>,
    @InjectRepository(ProductBranchEntity)
    private readonly productBranchRepository: Repository<ProductBranchEntity>,
    @InjectRepository(ProductCabysEntity)
    private readonly productCabysRepository: Repository<ProductCabysEntity>,
    @InjectRepository(ProductMotiveEntity)
    private readonly productMotiveRepository: Repository<ProductMotiveEntity>,
    @InjectRepository(ProductStateHistoryEntity)
    private readonly productStateHistoryRepository: Repository<ProductStateHistoryEntity>,
    @InjectRepository(UnitMeasureEntity)
    private readonly unitMeasureRepository: Repository<UnitMeasureEntity>,
    @InjectRepository(ReturnConditionEntity)
    private readonly returnConditionRepository: Repository<ReturnConditionEntity>,
    @InjectRepository(ProductStatusEntity)
    private readonly productStatusRepository: Repository<ProductStatusEntity>,
    @InjectRepository(BranchEntity)
    private readonly branchRepository: Repository<BranchEntity>,
    @InjectRepository(CabysEntity)
    private readonly cabysRepository: Repository<CabysEntity>,
    @InjectRepository(ProviderEntity)
    private readonly providerRepository: Repository<ProviderEntity>,
  ) {}

  async getUnitMeasures() {
    const items = await this.unitMeasureRepository.find({
      order: {
        unitMeasureId: 'ASC',
      },
    });

    return items.map((item) => ({
      id: item.unitMeasureId,
      nombre: item.unitMeasureName,
    }));
  }

  async getReturnConditions() {
    const items = await this.returnConditionRepository.find({
      order: {
        returnConditionId: 'ASC',
      },
    });

    return items.map((item) => ({
      id: item.returnConditionId,
      nombre: item.returnConditionName,
    }));
  }

  async getBranches() {
    const items = await this.branchRepository.find({
      order: {
        nombreSucursal: 'ASC',
      },
    });

    return items.map((item) => ({
      id: item.id,
      nombre: item.nombreSucursal,
    }));
  }

  async getCabys() {
    const items = await this.cabysRepository.find({
      order: {
        cabysName: 'ASC',
      },
    });

    return items.map((item) => ({
      id: item.cabysId,
      nombre: item.cabysName,
    }));
  }

  async getMyProducts(providerId: string | null) {
    if (!providerId) {
      throw new UnauthorizedException('El usuario no tiene proveedor asociado.');
    }

    const products = await this.productRepository.find({
      where: { providerId },
      relations: ['status', 'images', 'branches', 'branches.branch', 'cabys', 'cabys.cabys'],
      order: { createdAt: 'DESC' },
    });

    return products.map((product) => this.mapProductListItem(product));
  }

  async createProduct(
    providerId: string | null,
    dto: CreateProductDto,
    files: UploadedProductImage[] = [],
  ) {
    if (!providerId) {
      throw new UnauthorizedException('El usuario no tiene proveedor asociado.');
    }

    const normalizedSku = dto.idSku.trim();
    const existingProduct = await this.productRepository.findOne({
      where: {
        sku: normalizedSku,
      },
    });

    if (existingProduct) {
      throw new BadRequestException('Ya existe un producto con ese SKU.');
    }

    const [provider, unitMeasure, returnCondition, cabys, status] = await Promise.all([
      this.providerRepository.findOne({ where: { providerId } }),
      this.unitMeasureRepository.findOne({ where: { unitMeasureId: dto.idUnidadMedida } }),
      this.returnConditionRepository.findOne({
        where: { returnConditionId: dto.idCondicionesDevolucion },
      }),
      this.cabysRepository.findOne({ where: { cabysId: dto.idCabys.trim() } }),
      this.getDefaultStatus(),
    ]);

    if (!provider) {
      throw new BadRequestException('No se encontró el proveedor del usuario.');
    }

    if (!unitMeasure) {
      throw new BadRequestException('La unidad de medida no existe.');
    }

    if (!returnCondition) {
      throw new BadRequestException('La condición de devolución no existe.');
    }

    if (!status) {
      throw new BadRequestException('No se encontró el estado por defecto Enviado.');
    }

    if (!cabys) {
      throw new BadRequestException('El CABYS seleccionado no existe.');
    }

    const branchIds = Array.from(new Set(dto.idSucursales));
    if (branchIds.length === 0) {
      throw new BadRequestException('Debe seleccionar al menos una sucursal.');
    }

    const branches = await this.branchRepository.find({
      where: {
        id: In(branchIds),
      },
    });

    if (branches.length !== branchIds.length) {
      throw new BadRequestException('Una o más sucursales no existen.');
    }

    const product = this.productRepository.create({
      sku: normalizedSku,
      description: dto.descripcion?.trim() ?? null,
      unitMeasureId: dto.idUnidadMedida,
      returnConditionId: dto.idCondicionesDevolucion,
      statusId: status.statusId,
      providerId,
      vatPercentage: this.toNullableNumber(dto.porcIva),
      minDispatch: this.toNullableNumber(dto.minDespacho),
      packaging: this.toNullableNumber(dto.embalaje),
      gramaje: this.toNullableNumber(dto.gramaje),
      returnsSamples: this.toNullableBoolean(dto.devuelveMuestras),
      wantsAdvertising: this.toNullableBoolean(dto.deseaPublicidad),
      acceptsReturn: this.toNullableBoolean(dto.aceptaDevolucion),
      hasExchangePolicy: this.toNullableBoolean(dto.tienePoliticaCambios),
      paymentCondition: this.toNullableNumber(dto.condicionPago),
      costWithoutVat: this.toNullableNumber(dto.costoSinIva),
      costWithVat: this.toNullableNumber(dto.costoConIva),
      introDiscount: this.toNullableNumber(dto.descuentoIntroduccion),
      specialDiscount: this.toNullableNumber(dto.descuentoEspecial),
      permanentDiscount: this.toNullableNumber(dto.descuentoPermanente),
      suggestedMargin: this.toNullableNumber(dto.margenSugerido),
      height: this.toNullableNumber(dto.alto),
      width: this.toNullableNumber(dto.ancho),
      depth: this.toNullableNumber(dto.profundidad),
    });

    await this.productRepository.save(product);

    await this.productBranchRepository.save(
      branches.map((branch) =>
        this.productBranchRepository.create({
          sku: normalizedSku,
          branchId: branch.id,
        }),
      ),
    );

    await this.productCabysRepository.save(
      this.productCabysRepository.create({
        sku: normalizedSku,
        cabysId: cabys.cabysId,
        formPharmaceutical: dto.formaFarmaceutica?.trim() ?? '',
        medicineRegistry: dto.registroMedicamento?.trim() ?? '',
      }),
    );

    if (files.length > 0) {
      await this.saveImages(normalizedSku, files);
    }

    return this.getProductDetail(normalizedSku, providerId);
  }

  async getProductDetail(sku: string, providerId: string | null) {
    if (!providerId) {
      throw new UnauthorizedException('El usuario no tiene proveedor asociado.');
    }

    const product = await this.productRepository.findOne({
      where: {
        sku,
        providerId,
      },
      relations: [
        'unitMeasure',
        'returnCondition',
        'status',
        'images',
        'branches',
        'branches.branch',
        'cabys',
        'cabys.cabys',
      ],
    });

    if (!product) {
      throw new BadRequestException('Producto no encontrado.');
    }

    return {
      sku: product.sku,
      descripcion: product.description,
      idUnidadMedida: product.unitMeasureId,
      unidadMedida: product.unitMeasure?.unitMeasureName ?? null,
      idCondicionesDevolucion: product.returnConditionId,
      condicionDevolucion: product.returnCondition?.returnConditionName ?? null,
      idEstado: product.statusId,
      estado: product.status?.statusDescription ?? null,
      idProveedor: product.providerId,
      porcIva: this.toNumber(product.vatPercentage),
      minDespacho: this.toNumber(product.minDispatch),
      embalaje: this.toNumber(product.packaging),
      gramaje: this.toNumber(product.gramaje),
      devuelveMuestras: product.returnsSamples,
      deseaPublicidad: product.wantsAdvertising,
      aceptaDevolucion: product.acceptsReturn,
      tienePoliticaCambios: product.hasExchangePolicy,
      condicionPago: this.toNumber(product.paymentCondition),
      costoSinIva: this.toNumber(product.costWithoutVat),
      costoConIva: this.toNumber(product.costWithVat),
      descuentoIntroduccion: this.toNumber(product.introDiscount),
      descuentoEspecial: this.toNumber(product.specialDiscount),
      descuentoPermanente: this.toNumber(product.permanentDiscount),
      margenSugerido: this.toNumber(product.suggestedMargin),
      alto: this.toNumber(product.height),
      ancho: this.toNumber(product.width),
      profundidad: this.toNumber(product.depth),
      fechaCreacion: product.createdAt,
      sucursales: product.branches.map((item) => ({
        id: item.branchId,
        nombre: item.branch?.nombreSucursal ?? null,
      })),
      cabys: product.cabys.map((item) => ({
        id: item.cabysId,
        nombre: item.cabys?.cabysName ?? null,
        formaFarmaceutica: item.formPharmaceutical,
        registroMedicamento: item.medicineRegistry,
      })),
      imagenes: product.images.map((image) => ({
        nombreArchivo: image.fileName,
        archivo: image.filePath,
      })),
    };
  }

  async getProductProcess(sku: string, providerId: string | null) {
    if (!providerId) {
      throw new UnauthorizedException('El usuario no tiene proveedor asociado.');
    }

    const product = await this.productRepository.findOne({
      where: {
        sku,
        providerId,
      },
      relations: ['status', 'images', 'branches', 'branches.branch', 'cabys', 'cabys.cabys'],
    });

    if (!product) {
      throw new BadRequestException('Producto no encontrado.');
    }

    const history = await this.productStateHistoryRepository.find({
      where: { sku },
      relations: ['status', 'motive'],
      order: { createdAt: 'ASC', productStateId: 'ASC' },
    });

    return {
      articulo: this.mapProductListItem(product),
      detalle: {
        sku: product.sku,
        descripcion: product.description,
        idProveedor: product.providerId,
        fechaCreacion: product.createdAt,
        sucursales: product.branches.map((item) => ({
          id: item.branchId,
          nombre: item.branch?.nombreSucursal ?? null,
        })),
        cabys: product.cabys.map((item) => ({
          id: item.cabysId,
          nombre: item.cabys?.cabysName ?? null,
          formaFarmaceutica: item.formPharmaceutical,
          registroMedicamento: item.medicineRegistry,
        })),
      },
      estadoActual: {
        id: product.statusId,
        nombre: product.status?.statusDescription ?? null,
      },
      historial: history.map((item) => ({
        id: item.productStateId,
        estado: {
          id: item.statusId,
          nombre: item.status?.statusDescription ?? null,
        },
        motivo: {
          id: item.motiveId,
          descripcion: item.motive?.motiveDescription ?? null,
        },
        comentario: item.comment,
        fecha: item.createdAt,
      })),
    };
  }

  private async saveImages(sku: string, files: UploadedProductImage[]) {
    const productDirectory = join(process.cwd(), 'uploads', 'productos', sku);
    await mkdir(productDirectory, { recursive: true });

    const imageEntities = await Promise.all(
      files.map(async (file, index) => {
        const extension = this.getFileExtension(file.originalname);
        const fileName = `imagen-${index + 1}${extension}`.slice(0, 30);
        const relativePath = join('uploads', 'productos', sku, fileName).replace(/\\/g, '/');
        const absolutePath = join(process.cwd(), relativePath);

        await writeFile(absolutePath, file.buffer);

        return this.productImageRepository.create({
          sku,
          fileName,
          filePath: relativePath,
        });
      }),
    );

    await this.productImageRepository.save(imageEntities);
  }

  private getFileExtension(fileName: string) {
    const parts = fileName.split('.');
    if (parts.length < 2) {
      return '';
    }

    const extension = `.${parts.pop() ?? ''}`.toLowerCase();
    return extension.length > 5 ? '' : extension;
  }

  private toNullableNumber(value: unknown): number | null {
    if (value === null || value === undefined || value === '') {
      return null;
    }

    const numberValue = Number(value);
    if (Number.isNaN(numberValue)) {
      return null;
    }

    return numberValue;
  }

  private toNullableBoolean(value: unknown): boolean | null {
    if (value === null || value === undefined || value === '') {
      return null;
    }

    if (typeof value === 'boolean') {
      return value;
    }

    if (typeof value === 'number') {
      return value !== 0;
    }

    const normalized = String(value).trim().toLowerCase();
    return ['true', '1', 'si', 'sí', 's', 'on', 'yes'].includes(normalized);
  }

  private toNumber(value: number | string | null | undefined) {
    if (value === null || value === undefined) {
      return null;
    }

    return Number(value);
  }

  private async getDefaultStatus() {
    return this.productStatusRepository.findOne({
      where: [
        { statusDescription: 'Registrado' },
        { statusDescription: 'REGISTRADO' },
      ],
    });
  }

  private mapProductListItem(product: ProductEntity) {
    return {
      sku: product.sku,
      descripcion: product.description,
      estadoActual: {
        id: product.statusId,
        nombre: product.status?.statusDescription ?? null,
      },
      fechaCreacion: product.createdAt,
      imagenPrincipal: product.images[0]
        ? {
            nombreArchivo: product.images[0].fileName,
            archivo: product.images[0].filePath,
          }
        : null,
      sucursales: product.branches.map((item) => ({
        id: item.branchId,
        nombre: item.branch?.nombreSucursal ?? null,
      })),
      cabys: product.cabys.map((item) => ({
        id: item.cabysId,
        nombre: item.cabys?.cabysName ?? null,
      })),
    };
  }
}