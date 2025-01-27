using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class PreguntasIce
{
    public int IdPregunta { get; set; }

    public int IdCompetencia { get; set; }

    public string TextoPregunta { get; set; } = null!;

    public virtual ICollection<EncuestasIce> EncuestasIces { get; set; } = new List<EncuestasIce>();

    public virtual Competencia IdCompetenciaNavigation { get; set; } = null!;
}
