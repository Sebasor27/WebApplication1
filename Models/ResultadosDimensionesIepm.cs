using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ResultadosDimensionesIepm
{
    public int IdResultadoDimension { get; set; }

    public int IdEncuesta { get; set; }

    public int IdDimension { get; set; }

    public decimal Valor { get; set; }

    public DateTime? FechaCalculo { get; set; }

    public virtual DimensionesIepm IdDimensionNavigation { get; set; } = null!;

    public virtual EncuestasIepm IdEncuestaNavigation { get; set; } = null!;
}
