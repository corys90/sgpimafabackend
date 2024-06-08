using sgpimafaback.InventarioProducto.Domain.Entities;
using System;
using System.Collections.Generic;

namespace sgpimafaback.PosInventarioProducto.Domain.Entities;

public partial class PosinventarioproductoModel
{
    public int Id { get; set; }

    public int IdPos { get; set; }

    public int IdCodigo { get; set; }

    public int TipoProducto { get; set; }

    public int IdProductoCompuesto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public float? Cantidad { get; set; }

    public int? StockMinimo { get; set; }

    public int? UnidadMedida { get; set; }

    public string? Lote { get; set; }

    public string? Olor { get; set; }

    public string? Color { get; set; }

    public string? Textura { get; set; }

    public string? Tamano { get; set; }

    public float? Peso { get; set; }

    public int? Embalaje { get; set; }

    public float? Temperatura { get; set; }

    public int? ValorUnitario { get; set; }

    public float? Descuento { get; set; }

    public float? Impuesto { get; set; }

    public float? ValorIva { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? DiasVencimiento { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public string? User { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public static explicit operator PosinventarioproductoModel(inventarioproductoModel v)
    {
        throw new NotImplementedException();
    }
}
