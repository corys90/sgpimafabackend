using System;
using System.Collections.Generic;

namespace sgpimafaback.SedePos.Domain.Entities;

public partial class SedeposModel
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Direccion { get; set; }

    public float? Longitud { get; set; }

    public float? Latitud { get; set; }

    public int Estado { get; set; }

    public string? User { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
