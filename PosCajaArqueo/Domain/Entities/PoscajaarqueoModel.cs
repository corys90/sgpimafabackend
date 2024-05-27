using System;
using System.Collections.Generic;

namespace sgpimafaback.PosCajaArqueo.Domain.Entities;

public partial class PoscajaarqueoModel
{
    public int Id { get; set; }

    public int? IdCaja { get; set; }

    public int? IdPos { get; set; }

    public int? Valor { get; set; }

    public DateTime? FechaArqueo { get; set; }

    public int? EstadoArqueo { get; set; }

    public int? RevisorId { get; set; }

    public string? User { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
