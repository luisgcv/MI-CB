using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvDocumentosLinea
{
    public int IdLinea { get; set; }

    public string DocumentoConsecutivo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal? Monto { get; set; }

    public virtual TblProvDocumento DocumentoConsecutivoNavigation { get; set; } = null!;
}
