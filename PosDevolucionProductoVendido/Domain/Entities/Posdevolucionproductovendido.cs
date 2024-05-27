using System;
using System.Collections.Generic;

namespace sgpimafaback.PosDevolucionProductoVendido.Domain.Entities;

public partial class PosdevolucionproductovendidoModel
{
    public int Id { get; set; }

    public int IdPos { get; set; }

    public int IdFactura { get; set; }

    public int Nit { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int CodigoProducto { get; set; }

    public string Motivo { get; set; } = null!;

    public DateTime FechaDevolucion { get; set; }

    public string User { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
