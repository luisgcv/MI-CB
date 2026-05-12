using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvProductosMotivo
{
    public int IdMotivo { get; set; }

    public string? DescripcionMotivo { get; set; }

    public virtual ICollection<TblProvProductosEstado> TblProvProductosEstados { get; set; } = new List<TblProvProductosEstado>();
}
