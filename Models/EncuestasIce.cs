using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class EncuestasIce
{
    public int IdRespuesta { get; set; }

    public int IdEmprendedor { get; set; }

    public int IdPregunta { get; set; }

    public int ValorRespuesta { get; set; }

    public int IdEncuesta { get; set; }

    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;

    public virtual PreguntasIce IdPreguntaNavigation { get; set; } = null!;
}
