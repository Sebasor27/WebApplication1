using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class AccionesMejoraIepm
{
    public int IdAccion { get; set; }

    public decimal RangoMin { get; set; }

    public decimal RangoMax { get; set; }

    public string Descripcion { get; set; } = null!;

    public string? Recomendaciones { get; set; }

    public virtual ICollection<ResultadosAccionesIepm> ResultadosAccionesIepms { get; set; } = new List<ResultadosAccionesIepm>();
}
