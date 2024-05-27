using System;
using System.Collections.Generic;

namespace sgpimafaback.PosCajaEstado.Domain.Entities;

public partial class PoscajaestadoModel
{
    public int Id { get; set; }

    public int? IdCaja { get; set; }

    public int IdPos { get; set; }

    /// <summary>
    /// Valor $ del estado de la caja. Puede ser inciail/final
    /// </summary>
    public int ValorEstado { get; set; }

    public DateTime FechaOperacion { get; set; }

    public string? UserAccion { get; set; }

    public int Estado { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
