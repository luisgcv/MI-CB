using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvContratosLinea
{
    public int IdLinea { get; set; }

    public string ContatosConsecutivo { get; set; } = null!;

    public string LineaDescripcion { get; set; } = null!;

    public decimal? Cantidad { get; set; }

    public decimal? Monto { get; set; }

    public virtual TblProvContrato ContatosConsecutivoNavigation { get; set; } = null!;
}
