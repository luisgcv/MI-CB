using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvContrato
{
    public string ContatosConsecutivo { get; set; } = null!;

    public string TipoContrato { get; set; } = null!;

    public int IdSucursal { get; set; }

    public string IdProveedor { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaHasta { get; set; }

    public decimal? MontoMensual { get; set; }

    public virtual TblProvProveedore IdProveedorNavigation { get; set; } = null!;

    public virtual TblProvSucursale IdSucursalNavigation { get; set; } = null!;

    public virtual ICollection<TblProvContratosCobro> TblProvContratosCobros { get; set; } = new List<TblProvContratosCobro>();

    public virtual ICollection<TblProvContratosLinea> TblProvContratosLineas { get; set; } = new List<TblProvContratosLinea>();
}
