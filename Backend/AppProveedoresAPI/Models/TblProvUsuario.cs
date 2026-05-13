using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvUsuario
{
    public string IdIdentificacion { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? IpUltimoAcceso { get; set; }

    public DateTime? UltimoInicioSesion { get; set; }

    public DateTime? UltimoCambioContrasena { get; set; }

    public virtual ICollection<TblProvBitacoraSesion> TblProvBitacoraSesions { get; set; } = new List<TblProvBitacoraSesion>();

    public virtual ICollection<TblProvProveedoresDepartamentosUsuario> TblProvProveedoresDepartamentosUsuarios { get; set; } = new List<TblProvProveedoresDepartamentosUsuario>();

    public virtual TblProvUsuariosInformacion? TblProvUsuariosInformacion { get; set; }

    public virtual ICollection<TblProvPermiso> IdPermisos { get; set; } = new List<TblProvPermiso>();
}
