using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvUsuariosInformacion
{
    public string IdIdentificacion { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public virtual TblProvUsuario IdIdentificacionNavigation { get; set; } = null!;
}
