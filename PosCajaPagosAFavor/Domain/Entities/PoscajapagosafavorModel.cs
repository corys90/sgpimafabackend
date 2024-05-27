using System;
using System.Collections.Generic;

namespace sgpimafaback.PosCajaPagosAFavor.Domain.Entities;

public partial class PoscajapagosafavorModel
{
    public int Id { get; set; }

    public int IdCaja { get; set; }

    public int IdPos { get; set; }

    public int IdFactura { get; set; }

    public int IdPago { get; set; }

    public int SaldoAfavor { get; set; }

    public int Estado { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
