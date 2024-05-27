using System;
using System.Collections.Generic;

namespace sgpimafaback.PosFacturaDetalle.Domain.Entities;

public partial class PosfacturadetalleModel
{
    public int Id { get; set; }

    public int IdPos { get; set; }

    public int IdFactura { get; set; }

    public int? CodigoProducto { get; set; }

    public string? Descripcion { get; set; }

    public int? Cantidad { get; set; }

    public int? UnidadMedida { get; set; }

    public int? ValUnitario { get; set; }

    public int? Descuento { get; set; }

    public float? ValUnitarioDescuento { get; set; }

    public int? Iva { get; set; }

    public float? ValIva { get; set; }

    public float? SubTotal { get; set; }

    public float? Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
