using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvDinamicasLinea
{
    public int IdLinea { get; set; }

    public string DinamicasConsecutivo { get; set; } = null!;

    public string LineaArticulo { get; set; } = null!;

    public string LineaDescripcion { get; set; } = null!;

    public decimal? Cantidad { get; set; }

    public decimal? Monto { get; set; }

    public virtual TblProvDinamica DinamicasConsecutivoNavigation { get; set; } = null!;
}
