using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ResultadosAccionesIepm
{
    public int IdResultadoAccion { get; set; }

    public int IdResultadoIepm { get; set; }

    public int IdAccion { get; set; }

    public virtual AccionesMejoraIepm IdAccionNavigation { get; set; } = null!;

    public virtual ResultadosIepm IdResultadoIepmNavigation { get; set; } = null!;
}
