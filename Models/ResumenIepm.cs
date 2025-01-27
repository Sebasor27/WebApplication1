using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ResumenIepm
{
    public int IdEmprendedor { get; set; }

    public decimal ValorIepmTotal { get; set; }

    public int? IdAccionesIepm { get; set; }

    public virtual AccionesIepm? IdAccionesIepmNavigation { get; set; }

    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;
}
