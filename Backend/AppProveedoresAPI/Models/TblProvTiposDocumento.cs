using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvTiposDocumento
{
    public int IdTipoDocumentos { get; set; }

    public string? DescripcionTipoDocumentos { get; set; }

    public virtual ICollection<TblProvDocumento> TblProvDocumentos { get; set; } = new List<TblProvDocumento>();
}
