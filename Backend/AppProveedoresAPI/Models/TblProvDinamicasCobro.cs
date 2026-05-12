using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvDinamicasCobro
{
    public int IdCobro { get; set; }

    public string DinamicasConsecutivo { get; set; } = null!;

    public decimal? Monto { get; set; }

    public string Documento { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public virtual TblProvDinamica DinamicasConsecutivoNavigation { get; set; } = null!;

    public virtual TblProvDocumento DocumentoNavigation { get; set; } = null!;
}
