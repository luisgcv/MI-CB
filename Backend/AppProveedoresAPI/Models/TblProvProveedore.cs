using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvProveedore
{
    public string IdProveedor { get; set; } = null!;

    public string NombreProveedor { get; set; } = null!;

    public virtual ICollection<TblProvContrato> TblProvContratos { get; set; } = new List<TblProvContrato>();

    public virtual ICollection<TblProvDinamica> TblProvDinamicas { get; set; } = new List<TblProvDinamica>();

    public virtual ICollection<TblProvDocumento> TblProvDocumentos { get; set; } = new List<TblProvDocumento>();

    public virtual ICollection<TblProvOrdenesEncabezado> TblProvOrdenesEncabezados { get; set; } = new List<TblProvOrdenesEncabezado>();

    public virtual ICollection<TblProvProveedoresDepartamentosUsuario> TblProvProveedoresDepartamentosUsuarios { get; set; } = new List<TblProvProveedoresDepartamentosUsuario>();

    public virtual ICollection<TblProvReunione> TblProvReuniones { get; set; } = new List<TblProvReunione>();

    public virtual ICollection<TblProvSellout> TblProvSellouts { get; set; } = new List<TblProvSellout>();
}
