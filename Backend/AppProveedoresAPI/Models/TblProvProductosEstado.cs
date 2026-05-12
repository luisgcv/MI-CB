using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvProductosEstado
{
    public int IdProductoEstado { get; set; }

    public string IdSku { get; set; } = null!;

    public int IdEstado { get; set; }

    public int IdMotivo { get; set; }

    public string? Comentario { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual TblProvEstadosProducto IdEstadoNavigation { get; set; } = null!;

    public virtual TblProvProductosMotivo IdMotivoNavigation { get; set; } = null!;

    public virtual TblProvProducto IdSkuNavigation { get; set; } = null!;
}
