using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Indicadore
{
    public int IdIndicadores { get; set; }

    public string? Rango { get; set; }

    public string? Valoracion { get; set; }

    public string? NivelDesarrollo { get; set; }

    public string? Acciones { get; set; }

    public virtual ICollection<ResumenIce> ResumenIces { get; set; } = new List<ResumenIce>();
}
