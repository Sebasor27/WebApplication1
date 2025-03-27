using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class CriteriosEvaluacionIepm
{
    public int IdCriterio { get; set; }

    public int IdIndicador { get; set; }

    public int Valor { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual IndicadoresIepm IdIndicadorNavigation { get; set; } = null!;
}
