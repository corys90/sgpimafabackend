using System;
using System.Collections.Generic;

namespace sgpimafaback.UtiliatriesApi.Domain.Entities;

public partial class DtoUtilitariesModel
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Estado { get; set; }

}
