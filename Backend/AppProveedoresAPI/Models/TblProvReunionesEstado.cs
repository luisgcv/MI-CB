using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvReunionesEstado
{
    public int IdEstadoReunion { get; set; }

    public string? DescripcionEstadoReunion { get; set; }

    public virtual ICollection<TblProvReunione> TblProvReuniones { get; set; } = new List<TblProvReunione>();
}
