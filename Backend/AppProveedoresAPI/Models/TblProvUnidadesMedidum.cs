using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvUnidadesMedidum
{
    public int IdUnidadMedida { get; set; }

    public string NombreUnidadMedida { get; set; } = null!;

    public virtual ICollection<TblProvProducto> TblProvProductos { get; set; } = new List<TblProvProducto>();
}
