using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvCondicionesDevolucion
{
    public int IdCondicionesDevolucion { get; set; }

    public string NombreCondicionesDevolucion { get; set; } = null!;

    public virtual ICollection<TblProvProducto> TblProvProductos { get; set; } = new List<TblProvProducto>();
}
