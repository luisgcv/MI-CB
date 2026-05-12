using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvProductosCaby
{
    public string IdSku { get; set; } = null!;

    public string IdCabys { get; set; } = null!;

    public string FormaFarmaceutica { get; set; } = null!;

    public string RegistroMedicamento { get; set; } = null!;

    public virtual TblProvCaby IdCabysNavigation { get; set; } = null!;

    public virtual TblProvProducto IdSkuNavigation { get; set; } = null!;
}
