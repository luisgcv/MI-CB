using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvSelloutLinea
{
    public int IdLinea { get; set; }

    public string SelloutConsecutivo { get; set; } = null!;

    public DateTime FechaTransaccion { get; set; }

    public string Articulo { get; set; } = null!;

    public string ArticuloDescripcion { get; set; } = null!;

    public string ArticuloMarca { get; set; } = null!;

    public decimal? Cantidad { get; set; }

    public decimal? Monto { get; set; }

    public virtual TblProvSellout SelloutConsecutivoNavigation { get; set; } = null!;
}
