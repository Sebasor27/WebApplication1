using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ResultadosIepm
{
    public int IdResultado { get; set; }

    public int IdEmprendedor { get; set; }

    public int IdDimension { get; set; }

    public decimal PuntuacionDimension { get; set; }

    public virtual Dimensione IdDimensionNavigation { get; set; } = null!;

    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;
}
