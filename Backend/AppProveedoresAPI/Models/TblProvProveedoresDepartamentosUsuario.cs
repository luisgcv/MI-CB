using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvProveedoresDepartamentosUsuario
{
    public string IdProveedor { get; set; } = null!;

    public string IdIdentificacion { get; set; } = null!;

    public int IdDepartamento { get; set; }

    public string? Puesto { get; set; }

    public string? CorreoNotificacion { get; set; }

    public virtual TblProvDepartamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual TblProvUsuario IdIdentificacionNavigation { get; set; } = null!;

    public virtual TblProvProveedore IdProveedorNavigation { get; set; } = null!;
}
