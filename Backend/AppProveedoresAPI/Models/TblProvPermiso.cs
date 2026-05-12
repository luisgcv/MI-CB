using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvPermiso
{
    public int IdPermiso { get; set; }

    public string NombrePermiso { get; set; } = null!;

    public virtual ICollection<TblProvUsuario> IdIdentificacions { get; set; } = new List<TblProvUsuario>();
}
