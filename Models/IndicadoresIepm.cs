using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class IndicadoresIepm
{
    public int IdIndicador { get; set; }

    public int IdDimension { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Formula { get; set; }

    public decimal Peso { get; set; }

    public virtual ICollection<CriteriosEvaluacionIepm> CriteriosEvaluacionIepms { get; set; } = new List<CriteriosEvaluacionIepm>();

    public virtual DimensionesIepm IdDimensionNavigation { get; set; } = null!;

    public virtual ICollection<PreguntasIepm> PreguntasIepms { get; set; } = new List<PreguntasIepm>();

    public virtual ICollection<ResultadosIndicadoresIepm> ResultadosIndicadoresIepms { get; set; } = new List<ResultadosIndicadoresIepm>();
}
