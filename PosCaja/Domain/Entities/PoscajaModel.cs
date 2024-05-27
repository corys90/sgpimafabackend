using System;
using System.Collections.Generic;

namespace sgpimafaback.PosCaja.Domain.Entities;

public partial class PoscajaModel
{
    public int Id { get; set; }

    public int IdPos { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? User { get; set; }

    public int? Estado { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
