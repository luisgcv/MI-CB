using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvProductosImagene
{
    public string IdSku { get; set; } = null!;

    public string NombreArchivo { get; set; } = null!;

    public string Archivo { get; set; } = null!;

    public virtual TblProvProducto IdSkuNavigation { get; set; } = null!;
}
