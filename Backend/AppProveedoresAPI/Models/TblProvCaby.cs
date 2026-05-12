using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvCaby
{
    public string IdCabys { get; set; } = null!;

    public string NombreCabys { get; set; } = null!;

    public virtual ICollection<TblProvProductosCaby> TblProvProductosCabies { get; set; } = new List<TblProvProductosCaby>();
}
