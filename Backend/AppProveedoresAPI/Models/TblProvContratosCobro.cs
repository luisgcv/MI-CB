using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvContratosCobro
{
    public int IdCobro { get; set; }

    public string ContatosConsecutivo { get; set; } = null!;

    public decimal? Monto { get; set; }

    public string Documento { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public virtual TblProvContrato ContatosConsecutivoNavigation { get; set; } = null!;

    public virtual TblProvDocumento DocumentoNavigation { get; set; } = null!;
}
