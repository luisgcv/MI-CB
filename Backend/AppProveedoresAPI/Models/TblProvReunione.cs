using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvReunione
{
    public int Id { get; set; }

    public string IdProveedor { get; set; } = null!;

    public int IdTipoReunion { get; set; }

    public int IdDepartamento { get; set; }

    public int IdEstadoReunion { get; set; }

    public string Motivo { get; set; } = null!;

    public string Obervaciones { get; set; } = null!;

    public DateTime FechaHoraInicio { get; set; }

    public DateTime FechaHoraHasta { get; set; }

    public virtual TblProvDepartamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual TblProvReunionesEstado IdEstadoReunionNavigation { get; set; } = null!;

    public virtual TblProvProveedore IdProveedorNavigation { get; set; } = null!;

    public virtual TblProvReunionesTipo IdTipoReunionNavigation { get; set; } = null!;
}
