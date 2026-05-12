using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvEstadosDocumento
{
    public int IdEstadosDocumentos { get; set; }

    public string? DescripcionEstadosDocumentos { get; set; }

    public virtual ICollection<TblProvDocumento> TblProvDocumentos { get; set; } = new List<TblProvDocumento>();
}
