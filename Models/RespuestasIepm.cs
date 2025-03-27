using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class RespuestasIepm
{
    public int IdRespuesta { get; set; }

    public int IdEncuesta { get; set; }

    public int IdPregunta { get; set; }

    public int Valor { get; set; }

    public string? Comentarios { get; set; }

    public int? IdEmprendedor { get; set; }

    public virtual EncuestasIepm IdEncuestaNavigation { get; set; } = null!;

    public virtual PreguntasIepm IdPreguntaNavigation { get; set; } = null!;

    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;
}
