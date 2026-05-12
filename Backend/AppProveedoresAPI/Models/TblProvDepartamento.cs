using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvDepartamento
{
    public int IdDepartamento { get; set; }

    public string NombreDepartamento { get; set; } = null!;

    public string CorreosNotificacionReunion { get; set; } = null!;

    public virtual ICollection<TblProvProveedoresDepartamentosUsuario> TblProvProveedoresDepartamentosUsuarios { get; set; } = new List<TblProvProveedoresDepartamentosUsuario>();

    public virtual ICollection<TblProvReunione> TblProvReuniones { get; set; } = new List<TblProvReunione>();
}
