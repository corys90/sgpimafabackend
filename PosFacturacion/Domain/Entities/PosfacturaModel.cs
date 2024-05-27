using System;
using System.Collections.Generic;

namespace sgpimafaback.PosFacturacion.Domain.Entities;

public partial class PosfacturaModel
{
    public int Id { get; set; }

    public int IdPos { get; set; }

    public int IdFactura { get; set; }

    public string RazonSocial { get; set; } = null!;

    public int Nit { get; set; }

    public int TipoCliente { get; set; }

    public string Concepto { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime FechaFactura { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public int FormaPago { get; set; }

    public int IdVendedor { get; set; }

    public string User { get; set; } = null!;

    public float SubTotal { get; set; }

    public int Descuento { get; set; }

    public float Iva { get; set; }

    public float TotalOprecaion { get; set; }

    public float Retefuente { get; set; }

    public float ReteIca { get; set; }

    public float Total { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
