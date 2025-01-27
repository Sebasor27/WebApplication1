using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class PreguntasIepm
{
    public int IdPregunta { get; set; }

    public int IdDimension { get; set; }

    public string TextoPregunta { get; set; } = null!;

    public virtual ICollection<EncuestasIepm> EncuestasIepms { get; set; } = new List<EncuestasIepm>();

    public virtual Dimensione IdDimensionNavigation { get; set; } = null!;
}
