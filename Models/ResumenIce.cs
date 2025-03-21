using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ResumenIce
{
    public int IdEmprendedor { get; set; }

    public decimal ValorIceTotal { get; set; }

    public int? IdIndicadores { get; set; }
    public int IdEncuesta { get; set; }

    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;

    public virtual Indicadore? IdIndicadoresNavigation { get; set; }
}
