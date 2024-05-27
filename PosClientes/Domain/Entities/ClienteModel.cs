using System;
using System.Collections.Generic;

namespace sgpimafaback.PosClientes.Domain.Entities;

public partial class ClienteModel
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int TipoIdCliente { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Dpto { get; set; }

    public string Ciudad { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? User { get; set; }

    public short Estado { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
