using System;
using System.Collections.Generic;

namespace sgpimafaback.Models;

public partial class PosvendedorModel
{
    public int Id { get; set; }

    public int IdVendedor { get; set; }

    public int TipoIdVendedor { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string Ciudad { get; set; } = null!;

    public int? Estado { get; set; }

    public string? Dpto { get; set; }

    public string? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
