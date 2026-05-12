using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvBitacoraSesion
{
    public int IdBitacora { get; set; }

    public string IdIdentificacion { get; set; } = null!;

    public DateTime? FechaInicioSesion { get; set; }

    public virtual TblProvUsuario IdIdentificacionNavigation { get; set; } = null!;
}
