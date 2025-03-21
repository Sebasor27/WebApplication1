using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ResultadosIce
{
    public int IdResultado { get; set; }

    public int IdEmprendedor { get; set; }

    public int IdCompetencia { get; set; }

    public int Valoracion { get; set; }
    public decimal PuntuacionCompetencia { get; set; }
    public int IdEncuesta { get; set; }

    public virtual Competencia IdCompetenciaNavigation { get; set; } = null!;

    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;
}
