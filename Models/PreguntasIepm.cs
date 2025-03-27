using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class PreguntasIepm
{
    public int IdPregunta { get; set; }

    public int IdCuestionario { get; set; }

    public int IdIndicador { get; set; }

    public string Enunciado { get; set; } = null!;

    public int Orden { get; set; }

    public virtual CuestionariosIepm IdCuestionarioNavigation { get; set; } = null!;

    public virtual IndicadoresIepm IdIndicadorNavigation { get; set; } = null!;

    public virtual ICollection<RespuestasIepm> RespuestasIepms { get; set; } = new List<RespuestasIepm>();
}
