using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvSucursale
{
    public int IdSucursal { get; set; }

    public string NombreSucursal { get; set; } = null!;

    public virtual ICollection<TblProvContrato> TblProvContratos { get; set; } = new List<TblProvContrato>();

    public virtual ICollection<TblProvDinamica> TblProvDinamicas { get; set; } = new List<TblProvDinamica>();

    public virtual ICollection<TblProvOrdenesEncabezado> TblProvOrdenesEncabezados { get; set; } = new List<TblProvOrdenesEncabezado>();

    public virtual ICollection<TblProvSellout> TblProvSellouts { get; set; } = new List<TblProvSellout>();

    public virtual ICollection<TblProvProducto> IdSkus { get; set; } = new List<TblProvProducto>();
}
