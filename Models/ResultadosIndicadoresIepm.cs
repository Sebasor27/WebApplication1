using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ResultadosIndicadoresIepm
{
    public int IdResultadoIndicador { get; set; }

    public int IdEncuesta { get; set; }

    public int IdIndicador { get; set; }

    public decimal Valor { get; set; }

    public DateTime? FechaCalculo { get; set; }

    public virtual EncuestasIepm IdEncuestaNavigation { get; set; } = null!;

    public virtual IndicadoresIepm IdIndicadorNavigation { get; set; } = null!;
}
