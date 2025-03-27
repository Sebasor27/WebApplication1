using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class DimensionesIepm
{
    public int IdDimensionIepm { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Peso { get; set; }

    public string? Formula { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<IndicadoresIepm> IndicadoresIepms { get; set; } = new List<IndicadoresIepm>();

    public virtual ICollection<ResultadosDimensionesIepm> ResultadosDimensionesIepms { get; set; } = new List<ResultadosDimensionesIepm>();
}
