using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvDocumento
{
    public string DocumentoConsecutivo { get; set; } = null!;

    public string? IdProveedor { get; set; }

    public int IdTipoDocumentos { get; set; }

    public int IdEstadosDocumentos { get; set; }

    public DateTime FechaDocumento { get; set; }

    public DateTime FechaPago { get; set; }

    public decimal? Monto { get; set; }

    public virtual TblProvEstadosDocumento IdEstadosDocumentosNavigation { get; set; } = null!;

    public virtual TblProvProveedore? IdProveedorNavigation { get; set; }

    public virtual TblProvTiposDocumento IdTipoDocumentosNavigation { get; set; } = null!;

    public virtual ICollection<TblProvContratosCobro> TblProvContratosCobros { get; set; } = new List<TblProvContratosCobro>();

    public virtual ICollection<TblProvDinamicasCobro> TblProvDinamicasCobros { get; set; } = new List<TblProvDinamicasCobro>();

    public virtual ICollection<TblProvDocumentosLinea> TblProvDocumentosLineas { get; set; } = new List<TblProvDocumentosLinea>();
}
