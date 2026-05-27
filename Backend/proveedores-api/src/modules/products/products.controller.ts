import { Body, Controller, Get, Param, Post, Req, UploadedFiles, UseGuards, UseInterceptors } from '@nestjs/common';
import { FilesInterceptor } from '@nestjs/platform-express';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { CreateProductDto } from './dto/create-product.dto';
import { ProductsService } from './products.service';

type UploadedProductImage = {
  originalname: string;
  buffer: Buffer;
};

@Controller('productos')
@UseGuards(JwtAuthGuard)
export class ProductsController {
  constructor(private readonly productsService: ProductsService) {}

  @Get('unidades-medida')
  getUnitMeasures() {
    return this.productsService.getUnitMeasures();
  }

  @Get('condiciones-devolucion')
  getReturnConditions() {
    return this.productsService.getReturnConditions();
  }

  @Get('sucursales')
  getBranches() {
    return this.productsService.getBranches();
  }

  @Get('cabys')
  getCabys() {
    return this.productsService.getCabys();
  }

  @Get('mis-articulos')
  getMyProducts(@Req() req: any) {
    const providerId: string | null = req.user?.providerId ?? null;
    return this.productsService.getMyProducts(providerId);
  }

  @Get('mis-articulos/:sku')
  getMyProductDetail(@Req() req: any, @Param('sku') sku: string) {
    const providerId: string | null = req.user?.providerId ?? null;
    return this.productsService.getProductDetail(sku, providerId);
  }

  @Get('mis-articulos/:sku/historial')
  getMyProductHistory(@Req() req: any, @Param('sku') sku: string) {
    const providerId: string | null = req.user?.providerId ?? null;
    return this.productsService.getProductProcess(sku, providerId);
  }

  @Post()
  @UseInterceptors(FilesInterceptor('imagenes', 2))
  createProduct(
    @Req() req: any,
    @Body() dto: CreateProductDto,
    @UploadedFiles() files: UploadedProductImage[] = [],
  ) {
    const providerId: string | null = req.user?.providerId ?? null;
    return this.productsService.createProduct(providerId, dto, files);
  }
}