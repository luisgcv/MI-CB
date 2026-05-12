using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvReunionesTipo
{
    public int IdTipoReunion { get; set; }

    public string? DescripcionTipoReunion { get; set; }

    public virtual ICollection<TblProvReunione> TblProvReuniones { get; set; } = new List<TblProvReunione>();
}
