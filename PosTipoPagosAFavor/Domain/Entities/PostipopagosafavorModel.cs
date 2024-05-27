using System;
using System.Collections.Generic;

namespace sgpimafaback.PosTipoPagosAFavor.Domain.Entities;

public partial class PostipopagosafavorModel
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? User { get; set; }

    public short Estado { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
