using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class AccionesIepm
{
    public int IdAccionesIepm { get; set; }

    public string? Rango { get; set; }

    public string? Valoracion { get; set; }

    public virtual ICollection<ResumenIepm> ResumenIepms { get; set; } = new List<ResumenIepm>();
}
