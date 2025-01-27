using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Dimensione
{
    public int IdDimension { get; set; }

    public string NombreDimension { get; set; } = null!;

    public int PuntosMaximos { get; set; }

    public decimal PesoRelativo { get; set; }

    public virtual ICollection<PreguntasIepm> PreguntasIepms { get; set; } = new List<PreguntasIepm>();

    public virtual ICollection<ResultadosIepm> ResultadosIepms { get; set; } = new List<ResultadosIepm>();
}
