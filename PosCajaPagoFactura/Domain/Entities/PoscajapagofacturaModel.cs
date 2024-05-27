using System;
using System.Collections.Generic;

namespace sgpimafaback.PosCajaPagoFactura.Domain.Entities;

public partial class PoscajapagofacturaModel
{
    public int Id { get; set; }

    public int IdCaja { get; set; }

    public int IdPos { get; set; }

    public int IdFactura { get; set; }

    public int FormaPago { get; set; }

    public int ValorRecibido { get; set; }

    public int ValorDevuelto { get; set; }

    public int ValorPagado { get; set; }

    public DateTime FechaPago { get; set; }

    public string User { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
