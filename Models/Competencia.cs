using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Competencia
{
    public int IdCompetencia { get; set; }

    public string NombreCompetencia { get; set; } = null!;

    public int PuntosMaximos { get; set; }

    public decimal PesoRelativo { get; set; }

    public virtual ICollection<PreguntasIce> PreguntasIces { get; set; } = new List<PreguntasIce>();

    public virtual ICollection<ResultadosIce> ResultadosIces { get; set; } = new List<ResultadosIce>();
}
