using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppProveedoresAPI.Models;

public partial class AppProveedoresBdContext : DbContext
{
    public AppProveedoresBdContext()
    {
    }

    public AppProveedoresBdContext(DbContextOptions<AppProveedoresBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblProvBitacoraSesion> TblProvBitacoraSesions { get; set; }

    public virtual DbSet<TblProvCaby> TblProvCabys { get; set; }

    public virtual DbSet<TblProvCondicionesDevolucion> TblProvCondicionesDevolucions { get; set; }

    public virtual DbSet<TblProvContrato> TblProvContratos { get; set; }

    public virtual DbSet<TblProvContratosCobro> TblProvContratosCobros { get; set; }

    public virtual DbSet<TblProvContratosLinea> TblProvContratosLineas { get; set; }

    public virtual DbSet<TblProvDepartamento> TblProvDepartamentos { get; set; }

    public virtual DbSet<TblProvDinamica> TblProvDinamicas { get; set; }

    public virtual DbSet<TblProvDinamicasCobro> TblProvDinamicasCobros { get; set; }

    public virtual DbSet<TblProvDinamicasLinea> TblProvDinamicasLineas { get; set; }

    public virtual DbSet<TblProvDocumento> TblProvDocumentos { get; set; }

    public virtual DbSet<TblProvDocumentosLinea> TblProvDocumentosLineas { get; set; }

    public virtual DbSet<TblProvEstadoOrdene> TblProvEstadoOrdenes { get; set; }

    public virtual DbSet<TblProvEstadosDocumento> TblProvEstadosDocumentos { get; set; }

    public virtual DbSet<TblProvEstadosProducto> TblProvEstadosProductos { get; set; }

    public virtual DbSet<TblProvOrdenesEncabezado> TblProvOrdenesEncabezados { get; set; }

    public virtual DbSet<TblProvOrdenesLinea> TblProvOrdenesLineas { get; set; }

    public virtual DbSet<TblProvPermiso> TblProvPermisos { get; set; }

    public virtual DbSet<TblProvProducto> TblProvProductos { get; set; }

    public virtual DbSet<TblProvProductosCaby> TblProvProductosCabys { get; set; }

    public virtual DbSet<TblProvProductosEstado> TblProvProductosEstados { get; set; }

    public virtual DbSet<TblProvProductosImagene> TblProvProductosImagenes { get; set; }

    public virtual DbSet<TblProvProductosMotivo> TblProvProductosMotivos { get; set; }

    public virtual DbSet<TblProvProveedore> TblProvProveedores { get; set; }

    public virtual DbSet<TblProvProveedoresDepartamentosUsuario> TblProvProveedoresDepartamentosUsuarios { get; set; }

    public virtual DbSet<TblProvReunione> TblProvReuniones { get; set; }

    public virtual DbSet<TblProvReunionesEstado> TblProvReunionesEstados { get; set; }

    public virtual DbSet<TblProvReunionesTipo> TblProvReunionesTipos { get; set; }

    public virtual DbSet<TblProvSellout> TblProvSellouts { get; set; }

    public virtual DbSet<TblProvSelloutLinea> TblProvSelloutLineas { get; set; }

    public virtual DbSet<TblProvSucursale> TblProvSucursales { get; set; }

    public virtual DbSet<TblProvTiposDocumento> TblProvTiposDocumentos { get; set; }

    public virtual DbSet<TblProvUnidadesMedidum> TblProvUnidadesMedida { get; set; }

    public virtual DbSet<TblProvUsuario> TblProvUsuarios { get; set; }

    public virtual DbSet<TblProvUsuariosInformacion> TblProvUsuariosInformacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=APP_PROVEEDORES_BD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblProvBitacoraSesion>(entity =>
        {
            entity.HasKey(e => e.IdBitacora);

            entity.ToTable("TBL_PROV_BITACORA_SESION");

            entity.Property(e => e.IdBitacora).HasColumnName("ID_BITACORA");
            entity.Property(e => e.FechaInicioSesion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INICIO_SESION");
            entity.Property(e => e.IdIdentificacion)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_IDENTIFICACION");

            entity.HasOne(d => d.IdIdentificacionNavigation).WithMany(p => p.TblProvBitacoraSesions)
                .HasForeignKey(d => d.IdIdentificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_BITACORA_SESION_USUARIO");
        });

        modelBuilder.Entity<TblProvCaby>(entity =>
        {
            entity.HasKey(e => e.IdCabys);

            entity.ToTable("TBL_PROV_CABYS");

            entity.Property(e => e.IdCabys)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ID_CABYS");
            entity.Property(e => e.NombreCabys)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_CABYS");
        });

        modelBuilder.Entity<TblProvCondicionesDevolucion>(entity =>
        {
            entity.HasKey(e => e.IdCondicionesDevolucion);

            entity.ToTable("TBL_PROV_CONDICIONES_DEVOLUCION");

            entity.Property(e => e.IdCondicionesDevolucion)
                .ValueGeneratedNever()
                .HasColumnName("ID_CONDICIONES_DEVOLUCION");
            entity.Property(e => e.NombreCondicionesDevolucion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_CONDICIONES_DEVOLUCION");
        });

        modelBuilder.Entity<TblProvContrato>(entity =>
        {
            entity.HasKey(e => e.ContatosConsecutivo);

            entity.ToTable("TBL_PROV_CONTRATOS");

            entity.Property(e => e.ContatosConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CONTATOS_CONSECUTIVO");
            entity.Property(e => e.FechaHasta)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_HASTA");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INICIO");
            entity.Property(e => e.IdProveedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.IdSucursal).HasColumnName("ID_SUCURSAL");
            entity.Property(e => e.MontoMensual)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO_MENSUAL");
            entity.Property(e => e.TipoContrato)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIPO_CONTRATO");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblProvContratos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_CONTRATOS_PROVEEDOR");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.TblProvContratos)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_CONTRATOS_SUCURSAL");
        });

        modelBuilder.Entity<TblProvContratosCobro>(entity =>
        {
            entity.HasKey(e => e.IdCobro);

            entity.ToTable("TBL_PROV_CONTRATOS_COBROS");

            entity.Property(e => e.IdCobro).HasColumnName("ID_COBRO");
            entity.Property(e => e.ContatosConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CONTATOS_CONSECUTIVO");
            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.Monto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO");

            entity.HasOne(d => d.ContatosConsecutivoNavigation).WithMany(p => p.TblProvContratosCobros)
                .HasForeignKey(d => d.ContatosConsecutivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_CONTRATOS_COBROS_CONTRATO");

            entity.HasOne(d => d.DocumentoNavigation).WithMany(p => p.TblProvContratosCobros)
                .HasForeignKey(d => d.Documento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_CONTRATOS_COBROS_DOCUMENTO");
        });

        modelBuilder.Entity<TblProvContratosLinea>(entity =>
        {
            entity.HasKey(e => e.IdLinea);

            entity.ToTable("TBL_PROV_CONTRATOS_LINEAS");

            entity.Property(e => e.IdLinea).HasColumnName("ID_LINEA");
            entity.Property(e => e.Cantidad)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("CANTIDAD");
            entity.Property(e => e.ContatosConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CONTATOS_CONSECUTIVO");
            entity.Property(e => e.LineaDescripcion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LINEA_DESCRIPCION");
            entity.Property(e => e.Monto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO");

            entity.HasOne(d => d.ContatosConsecutivoNavigation).WithMany(p => p.TblProvContratosLineas)
                .HasForeignKey(d => d.ContatosConsecutivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_CONTRATOS_LINEAS_CONTRATO");
        });

        modelBuilder.Entity<TblProvDepartamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento);

            entity.ToTable("TBL_PROV_DEPARTAMENTOS");

            entity.Property(e => e.IdDepartamento)
                .ValueGeneratedNever()
                .HasColumnName("ID_DEPARTAMENTO");
            entity.Property(e => e.CorreosNotificacionReunion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("CORREOS_NOTIFICACION_REUNION");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_DEPARTAMENTO");
        });

        modelBuilder.Entity<TblProvDinamica>(entity =>
        {
            entity.HasKey(e => e.DinamicasConsecutivo);

            entity.ToTable("TBL_PROV_DINAMICAS");

            entity.Property(e => e.DinamicasConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DINAMICAS_CONSECUTIVO");
            entity.Property(e => e.FechaHasta)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_HASTA");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INICIO");
            entity.Property(e => e.IdProveedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.IdSucursal).HasColumnName("ID_SUCURSAL");
            entity.Property(e => e.MontoTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO_TOTAL");
            entity.Property(e => e.TipoDinamica)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIPO_DINAMICA");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblProvDinamicas)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_DINAMICAS_PROVEEDOR");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.TblProvDinamicas)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_DINAMICAS_SUCURSAL");
        });

        modelBuilder.Entity<TblProvDinamicasCobro>(entity =>
        {
            entity.HasKey(e => e.IdCobro);

            entity.ToTable("TBL_PROV_DINAMICAS_COBROS");

            entity.Property(e => e.IdCobro).HasColumnName("ID_COBRO");
            entity.Property(e => e.DinamicasConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DINAMICAS_CONSECUTIVO");
            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.Monto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO");

            entity.HasOne(d => d.DinamicasConsecutivoNavigation).WithMany(p => p.TblProvDinamicasCobros)
                .HasForeignKey(d => d.DinamicasConsecutivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_DINAMICAS_COBROS_DINAMICA");

            entity.HasOne(d => d.DocumentoNavigation).WithMany(p => p.TblProvDinamicasCobros)
                .HasForeignKey(d => d.Documento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_DINAMICAS_COBROS_DOCUMENTO");
        });

        modelBuilder.Entity<TblProvDinamicasLinea>(entity =>
        {
            entity.HasKey(e => e.IdLinea);

            entity.ToTable("TBL_PROV_DINAMICAS_LINEAS");

            entity.Property(e => e.IdLinea).HasColumnName("ID_LINEA");
            entity.Property(e => e.Cantidad)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("CANTIDAD");
            entity.Property(e => e.DinamicasConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DINAMICAS_CONSECUTIVO");
            entity.Property(e => e.LineaArticulo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LINEA_ARTICULO");
            entity.Property(e => e.LineaDescripcion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LINEA_DESCRIPCION");
            entity.Property(e => e.Monto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO");

            entity.HasOne(d => d.DinamicasConsecutivoNavigation).WithMany(p => p.TblProvDinamicasLineas)
                .HasForeignKey(d => d.DinamicasConsecutivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_DINAMICAS_LINEAS_DINAMICA");
        });

        modelBuilder.Entity<TblProvDocumento>(entity =>
        {
            entity.HasKey(e => e.DocumentoConsecutivo);

            entity.ToTable("TBL_PROV_DOCUMENTOS");

            entity.Property(e => e.DocumentoConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO_CONSECUTIVO");
            entity.Property(e => e.FechaDocumento)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_DOCUMENTO");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_PAGO");
            entity.Property(e => e.IdEstadosDocumentos).HasColumnName("ID_ESTADOS_DOCUMENTOS");
            entity.Property(e => e.IdProveedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.IdTipoDocumentos).HasColumnName("ID_TIPO_DOCUMENTOS");
            entity.Property(e => e.Monto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO");

            entity.HasOne(d => d.IdEstadosDocumentosNavigation).WithMany(p => p.TblProvDocumentos)
                .HasForeignKey(d => d.IdEstadosDocumentos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_DOCUMENTOS_ESTADO");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblProvDocumentos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_TBL_PROV_DOCUMENTOS_PROVEEDOR");

            entity.HasOne(d => d.IdTipoDocumentosNavigation).WithMany(p => p.TblProvDocumentos)
                .HasForeignKey(d => d.IdTipoDocumentos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_DOCUMENTOS_TIPO");
        });

        modelBuilder.Entity<TblProvDocumentosLinea>(entity =>
        {
            entity.HasKey(e => e.IdLinea);

            entity.ToTable("TBL_PROV_DOCUMENTOS_LINEAS");

            entity.Property(e => e.IdLinea).HasColumnName("ID_LINEA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.DocumentoConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO_CONSECUTIVO");
            entity.Property(e => e.Monto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO");

            entity.HasOne(d => d.DocumentoConsecutivoNavigation).WithMany(p => p.TblProvDocumentosLineas)
                .HasForeignKey(d => d.DocumentoConsecutivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_DOCUMENTOS_LINEAS_DOCUMENTO");
        });

        modelBuilder.Entity<TblProvEstadoOrdene>(entity =>
        {
            entity.HasKey(e => e.IdEstadoOrden);

            entity.ToTable("TBL_PROV_ESTADO_ORDENES");

            entity.Property(e => e.IdEstadoOrden)
                .ValueGeneratedNever()
                .HasColumnName("ID_ESTADO_ORDEN");
            entity.Property(e => e.DescripcionOrden)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION_ORDEN");
        });

        modelBuilder.Entity<TblProvEstadosDocumento>(entity =>
        {
            entity.HasKey(e => e.IdEstadosDocumentos);

            entity.ToTable("TBL_PROV_ESTADOS_DOCUMENTOS");

            entity.Property(e => e.IdEstadosDocumentos)
                .ValueGeneratedNever()
                .HasColumnName("ID_ESTADOS_DOCUMENTOS");
            entity.Property(e => e.DescripcionEstadosDocumentos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION_ESTADOS_DOCUMENTOS");
        });

        modelBuilder.Entity<TblProvEstadosProducto>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("TBL_PROV_ESTADOS_PRODUCTOS");

            entity.Property(e => e.IdEstado)
                .ValueGeneratedNever()
                .HasColumnName("ID_ESTADO");
            entity.Property(e => e.DescripcionEstado)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION_ESTADO");
        });

        modelBuilder.Entity<TblProvOrdenesEncabezado>(entity =>
        {
            entity.HasKey(e => e.IdOrden);

            entity.ToTable("TBL_PROV_ORDENES_ENCABEZADO");

            entity.Property(e => e.IdOrden)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ID_ORDEN");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CREACION");
            entity.Property(e => e.FechaRequerida)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_REQUERIDA");
            entity.Property(e => e.IdEstadoOrden).HasColumnName("ID_ESTADO_ORDEN");
            entity.Property(e => e.IdProveedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.IdSucursal).HasColumnName("ID_SUCURSAL");
            entity.Property(e => e.MontoTotal)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO_TOTAL");
            entity.Property(e => e.NombreCreador)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_CREADOR");

            entity.HasOne(d => d.IdEstadoOrdenNavigation).WithMany(p => p.TblProvOrdenesEncabezados)
                .HasForeignKey(d => d.IdEstadoOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_ORDENES_ENCABEZADO_ESTADO");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblProvOrdenesEncabezados)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_TBL_PROV_ORDENES_ENCABEZADO_PROVEEDOR");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.TblProvOrdenesEncabezados)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_ORDENES_ENCABEZADO_SUCURSAL");
        });

        modelBuilder.Entity<TblProvOrdenesLinea>(entity =>
        {
            entity.HasKey(e => new { e.IdOrden, e.SkuOrden });

            entity.ToTable("TBL_PROV_ORDENES_LINEAS");

            entity.Property(e => e.IdOrden)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ID_ORDEN");
            entity.Property(e => e.SkuOrden)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SKU_ORDEN");
            entity.Property(e => e.CantidadBackorder)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("CANTIDAD_BACKORDER");
            entity.Property(e => e.CantidadDevolucion)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("CANTIDAD_DEVOLUCION");
            entity.Property(e => e.CantidadOrdenada)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("CANTIDAD_ORDENADA");
            entity.Property(e => e.CantidadRecibida)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("CANTIDAD_RECIBIDA");
            entity.Property(e => e.Costo)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("COSTO");
            entity.Property(e => e.SkuOrdenDescripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SKU_ORDEN_DESCRIPCION");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.TblProvOrdenesLineas)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_ORDENES_LINEAS_ENCABEZADO");
        });

        modelBuilder.Entity<TblProvPermiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso);

            entity.ToTable("TBL_PROV_PERMISOS");

            entity.Property(e => e.IdPermiso)
                .ValueGeneratedNever()
                .HasColumnName("ID_PERMISO");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PERMISO");

            entity.HasMany(d => d.IdIdentificacions).WithMany(p => p.IdPermisos)
                .UsingEntity<Dictionary<string, object>>(
                    "TblProvPerfile",
                    r => r.HasOne<TblProvUsuario>().WithMany()
                        .HasForeignKey("IdIdentificacion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TBL_PROV_PERFILES_USUARIO"),
                    l => l.HasOne<TblProvPermiso>().WithMany()
                        .HasForeignKey("IdPermiso")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TBL_PROV_PERFILES_PERMISO"),
                    j =>
                    {
                        j.HasKey("IdPermiso", "IdIdentificacion");
                        j.ToTable("TBL_PROV_PERFILES");
                        j.IndexerProperty<int>("IdPermiso").HasColumnName("ID_PERMISO");
                        j.IndexerProperty<string>("IdIdentificacion")
                            .HasMaxLength(15)
                            .IsUnicode(false)
                            .HasColumnName("ID_IDENTIFICACION");
                    });
        });

        modelBuilder.Entity<TblProvProducto>(entity =>
        {
            entity.HasKey(e => e.IdSku);

            entity.ToTable("TBL_PROV_PRODUCTOS");

            entity.Property(e => e.IdSku)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_SKU");
            entity.Property(e => e.AceptaDevolucion)
                .HasDefaultValue(false)
                .HasColumnName("ACEPTA_DEVOLUCION");
            entity.Property(e => e.Alto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ALTO");
            entity.Property(e => e.Ancho)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ANCHO");
            entity.Property(e => e.CondicionPago)
                .HasDefaultValue(0)
                .HasColumnName("CONDICION_PAGO");
            entity.Property(e => e.CostoConIva)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("COSTO_CON_IVA");
            entity.Property(e => e.CostoSinIva)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("COSTO_SIN_IVA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.DescuentoEspecial)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("DESCUENTO_ESPECIAL");
            entity.Property(e => e.DescuentoIntroduccion)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("DESCUENTO_INTRODUCCION");
            entity.Property(e => e.DescuentoPermanente)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("DESCUENTO_PERMANENTE");
            entity.Property(e => e.DeseaPublicidad)
                .HasDefaultValue(false)
                .HasColumnName("DESEA_PUBLICIDAD");
            entity.Property(e => e.DevuelveMuestras)
                .HasDefaultValue(false)
                .HasColumnName("DEVUELVE_MUESTRAS");
            entity.Property(e => e.Embalaje)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("EMBALAJE");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CREACION");
            entity.Property(e => e.Gramaje)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("GRAMAJE");
            entity.Property(e => e.IdCondicionesDevolucion).HasColumnName("ID_CONDICIONES_DEVOLUCION");
            entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");
            entity.Property(e => e.IdUnidadMedida).HasColumnName("ID_UNIDAD_MEDIDA");
            entity.Property(e => e.MargenSugerido)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MARGEN_SUGERIDO");
            entity.Property(e => e.MinDespacho)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MIN_DESPACHO");
            entity.Property(e => e.PorcIva)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("PORC_IVA");
            entity.Property(e => e.Profundidad)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("PROFUNDIDAD");
            entity.Property(e => e.TienePoliticaCambios)
                .HasDefaultValue(false)
                .HasColumnName("TIENE_POLITICA_CAMBIOS");

            entity.HasOne(d => d.IdCondicionesDevolucionNavigation).WithMany(p => p.TblProvProductos)
                .HasForeignKey(d => d.IdCondicionesDevolucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PRODUCTOS_CONDICION_DEVOLUCION");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.TblProvProductos)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PRODUCTOS_ESTADO");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.TblProvProductos)
                .HasForeignKey(d => d.IdUnidadMedida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PRODUCTOS_UNIDAD_MEDIDA");

            entity.HasMany(d => d.IdSucursals).WithMany(p => p.IdSkus)
                .UsingEntity<Dictionary<string, object>>(
                    "TblProvProductosSucursale",
                    r => r.HasOne<TblProvSucursale>().WithMany()
                        .HasForeignKey("IdSucursal")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TBL_PROV_PRODUCTOS_SUCURSALES_SUCURSAL"),
                    l => l.HasOne<TblProvProducto>().WithMany()
                        .HasForeignKey("IdSku")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TBL_PROV_PRODUCTOS_SUCURSALES_PRODUCTO"),
                    j =>
                    {
                        j.HasKey("IdSku", "IdSucursal");
                        j.ToTable("TBL_PROV_PRODUCTOS_SUCURSALES");
                        j.IndexerProperty<string>("IdSku")
                            .HasMaxLength(15)
                            .IsUnicode(false)
                            .HasColumnName("ID_SKU");
                        j.IndexerProperty<int>("IdSucursal").HasColumnName("ID_SUCURSAL");
                    });
        });

        modelBuilder.Entity<TblProvProductosCaby>(entity =>
        {
            entity.HasKey(e => new { e.IdSku, e.IdCabys });

            entity.ToTable("TBL_PROV_PRODUCTOS_CABYS");

            entity.Property(e => e.IdSku)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_SKU");
            entity.Property(e => e.IdCabys)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ID_CABYS");
            entity.Property(e => e.FormaFarmaceutica)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("FORMA_FARMACEUTICA");
            entity.Property(e => e.RegistroMedicamento)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("REGISTRO_MEDICAMENTO");

            entity.HasOne(d => d.IdCabysNavigation).WithMany(p => p.TblProvProductosCabies)
                .HasForeignKey(d => d.IdCabys)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PRODUCTOS_CABYS_CABYS");

            entity.HasOne(d => d.IdSkuNavigation).WithMany(p => p.TblProvProductosCabies)
                .HasForeignKey(d => d.IdSku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PRODUCTOS_CABYS_PRODUCTO");
        });

        modelBuilder.Entity<TblProvProductosEstado>(entity =>
        {
            entity.HasKey(e => e.IdProductoEstado);

            entity.ToTable("TBL_PROV_PRODUCTOS_ESTADOS");

            entity.Property(e => e.IdProductoEstado).HasColumnName("ID_PRODUCTO_ESTADO");
            entity.Property(e => e.Comentario)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("COMENTARIO");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");
            entity.Property(e => e.IdMotivo).HasColumnName("ID_MOTIVO");
            entity.Property(e => e.IdSku)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_SKU");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.TblProvProductosEstados)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PRODUCTOS_ESTADOS_ESTADO");

            entity.HasOne(d => d.IdMotivoNavigation).WithMany(p => p.TblProvProductosEstados)
                .HasForeignKey(d => d.IdMotivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PRODUCTOS_ESTADOS_MOTIVO");

            entity.HasOne(d => d.IdSkuNavigation).WithMany(p => p.TblProvProductosEstados)
                .HasForeignKey(d => d.IdSku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PRODUCTOS_ESTADOS_PRODUCTO");
        });

        modelBuilder.Entity<TblProvProductosImagene>(entity =>
        {
            entity.HasKey(e => new { e.IdSku, e.NombreArchivo });

            entity.ToTable("TBL_PROV_PRODUCTOS_IMAGENES");

            entity.Property(e => e.IdSku)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_SKU");
            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_ARCHIVO");
            entity.Property(e => e.Archivo)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("ARCHIVO");

            entity.HasOne(d => d.IdSkuNavigation).WithMany(p => p.TblProvProductosImagenes)
                .HasForeignKey(d => d.IdSku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PRODUCTOS_IMAGENES_PRODUCTO");
        });

        modelBuilder.Entity<TblProvProductosMotivo>(entity =>
        {
            entity.HasKey(e => e.IdMotivo);

            entity.ToTable("TBL_PROV_PRODUCTOS_MOTIVOS");

            entity.Property(e => e.IdMotivo)
                .ValueGeneratedNever()
                .HasColumnName("ID_MOTIVO");
            entity.Property(e => e.DescripcionMotivo)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION_MOTIVO");
        });

        modelBuilder.Entity<TblProvProveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor);

            entity.ToTable("TBL_PROV_PROVEEDORES");

            entity.Property(e => e.IdProveedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PROVEEDOR");
        });

        modelBuilder.Entity<TblProvProveedoresDepartamentosUsuario>(entity =>
        {
            entity.HasKey(e => new { e.IdProveedor, e.IdIdentificacion, e.IdDepartamento });

            entity.ToTable("TBL_PROV_PROVEEDORES_DEPARTAMENTOS_USUARIO");

            entity.Property(e => e.IdProveedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.IdIdentificacion)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_IDENTIFICACION");
            entity.Property(e => e.IdDepartamento).HasColumnName("ID_DEPARTAMENTO");
            entity.Property(e => e.CorreoNotificacion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CORREO_NOTIFICACION");
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PUESTO");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.TblProvProveedoresDepartamentosUsuarios)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PDU_DEPARTAMENTO");

            entity.HasOne(d => d.IdIdentificacionNavigation).WithMany(p => p.TblProvProveedoresDepartamentosUsuarios)
                .HasForeignKey(d => d.IdIdentificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PDU_USUARIO");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblProvProveedoresDepartamentosUsuarios)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_PDU_PROVEEDOR");
        });

        modelBuilder.Entity<TblProvReunione>(entity =>
        {
            entity.ToTable("TBL_PROV_REUNIONES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FechaHoraHasta)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_HORA_HASTA");
            entity.Property(e => e.FechaHoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_HORA_INICIO");
            entity.Property(e => e.IdDepartamento).HasColumnName("ID_DEPARTAMENTO");
            entity.Property(e => e.IdEstadoReunion).HasColumnName("ID_ESTADO_REUNION");
            entity.Property(e => e.IdProveedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.IdTipoReunion).HasColumnName("ID_TIPO_REUNION");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MOTIVO");
            entity.Property(e => e.Obervaciones)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("OBERVACIONES");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.TblProvReuniones)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_REUNIONES_DEPARTAMENTO");

            entity.HasOne(d => d.IdEstadoReunionNavigation).WithMany(p => p.TblProvReuniones)
                .HasForeignKey(d => d.IdEstadoReunion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_REUNIONES_ESTADO");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblProvReuniones)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_REUNIONES_PROVEEDOR");

            entity.HasOne(d => d.IdTipoReunionNavigation).WithMany(p => p.TblProvReuniones)
                .HasForeignKey(d => d.IdTipoReunion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_REUNIONES_TIPO");
        });

        modelBuilder.Entity<TblProvReunionesEstado>(entity =>
        {
            entity.HasKey(e => e.IdEstadoReunion);

            entity.ToTable("TBL_PROV_REUNIONES_ESTADOS");

            entity.Property(e => e.IdEstadoReunion)
                .ValueGeneratedNever()
                .HasColumnName("ID_ESTADO_REUNION");
            entity.Property(e => e.DescripcionEstadoReunion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION_ESTADO_REUNION");
        });

        modelBuilder.Entity<TblProvReunionesTipo>(entity =>
        {
            entity.HasKey(e => e.IdTipoReunion);

            entity.ToTable("TBL_PROV_REUNIONES_TIPOS");

            entity.Property(e => e.IdTipoReunion)
                .ValueGeneratedNever()
                .HasColumnName("ID_TIPO_REUNION");
            entity.Property(e => e.DescripcionTipoReunion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION_TIPO_REUNION");
        });

        modelBuilder.Entity<TblProvSellout>(entity =>
        {
            entity.HasKey(e => e.SelloutConsecutivo);

            entity.ToTable("TBL_PROV_SELLOUT");

            entity.Property(e => e.SelloutConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SELLOUT_CONSECUTIVO");
            entity.Property(e => e.FechaVigenciaHasta)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_VIGENCIA_HASTA");
            entity.Property(e => e.FechaVigenciaInicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_VIGENCIA_INICIO");
            entity.Property(e => e.IdProveedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_PROVEEDOR");
            entity.Property(e => e.IdSucursal).HasColumnName("ID_SUCURSAL");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TblProvSellouts)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_SELLOUT_PROVEEDOR");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.TblProvSellouts)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_SELLOUT_SUCURSAL");
        });

        modelBuilder.Entity<TblProvSelloutLinea>(entity =>
        {
            entity.HasKey(e => e.IdLinea);

            entity.ToTable("TBL_PROV_SELLOUT_LINEAS");

            entity.Property(e => e.IdLinea).HasColumnName("ID_LINEA");
            entity.Property(e => e.Articulo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ARTICULO");
            entity.Property(e => e.ArticuloDescripcion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ARTICULO_DESCRIPCION");
            entity.Property(e => e.ArticuloMarca)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ARTICULO_MARCA");
            entity.Property(e => e.Cantidad)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("CANTIDAD");
            entity.Property(e => e.FechaTransaccion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_TRANSACCION");
            entity.Property(e => e.Monto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MONTO");
            entity.Property(e => e.SelloutConsecutivo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SELLOUT_CONSECUTIVO");

            entity.HasOne(d => d.SelloutConsecutivoNavigation).WithMany(p => p.TblProvSelloutLineas)
                .HasForeignKey(d => d.SelloutConsecutivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_SELLOUT_LINEAS_SELLOUT");
        });

        modelBuilder.Entity<TblProvSucursale>(entity =>
        {
            entity.HasKey(e => e.IdSucursal);

            entity.ToTable("TBL_PROV_SUCURSALES");

            entity.Property(e => e.IdSucursal)
                .ValueGeneratedNever()
                .HasColumnName("ID_SUCURSAL");
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_SUCURSAL");
        });

        modelBuilder.Entity<TblProvTiposDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumentos);

            entity.ToTable("TBL_PROV_TIPOS_DOCUMENTOS");

            entity.Property(e => e.IdTipoDocumentos)
                .ValueGeneratedNever()
                .HasColumnName("ID_TIPO_DOCUMENTOS");
            entity.Property(e => e.DescripcionTipoDocumentos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION_TIPO_DOCUMENTOS");
        });

        modelBuilder.Entity<TblProvUnidadesMedidum>(entity =>
        {
            entity.HasKey(e => e.IdUnidadMedida);

            entity.ToTable("TBL_PROV_UNIDADES_MEDIDA");

            entity.Property(e => e.IdUnidadMedida)
                .ValueGeneratedNever()
                .HasColumnName("ID_UNIDAD_MEDIDA");
            entity.Property(e => e.NombreUnidadMedida)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_UNIDAD_MEDIDA");
        });

        modelBuilder.Entity<TblProvUsuario>(entity =>
        {
            entity.HasKey(e => e.IdIdentificacion);

            entity.ToTable("TBL_PROV_USUARIOS");

            entity.Property(e => e.IdIdentificacion)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_IDENTIFICACION");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("CONTRASEÑA");
            entity.Property(e => e.IpUltimoAcceso)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("IP_ULTIMO_ACCESO");
            entity.Property(e => e.UltimoCambioContraseña)
                .HasColumnType("datetime")
                .HasColumnName("ULTIMO_CAMBIO_CONTRASEÑA");
            entity.Property(e => e.UltimoInicioSesion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ULTIMO_INICIO_SESION");
        });

        modelBuilder.Entity<TblProvUsuariosInformacion>(entity =>
        {
            entity.HasKey(e => e.IdIdentificacion);

            entity.ToTable("TBL_PROV_USUARIOS_INFORMACION");

            entity.Property(e => e.IdIdentificacion)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID_IDENTIFICACION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Telefono)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdIdentificacionNavigation).WithOne(p => p.TblProvUsuariosInformacion)
                .HasForeignKey<TblProvUsuariosInformacion>(d => d.IdIdentificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PROV_USUARIOS_INFORMACION_USUARIO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
