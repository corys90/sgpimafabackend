using System;
using System.Collections.Generic;

namespace sgpimafaback.PosMovimientoInventario.Domain.Entities;

public partial class PosmovimientoinventarioModel
{
    public int Id { get; set; }

    public int IdPos { get; set; }

    public int IdCodigo { get; set; }

    public int Cantidad { get; set; }

    public DateTime FechaMovimiento { get; set; }

    public string User { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
