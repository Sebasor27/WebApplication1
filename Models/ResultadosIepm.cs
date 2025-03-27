using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ResultadosIepm
{
    public int IdResultadoIepm { get; set; }

    public int IdEncuesta { get; set; }

    public int IdEmprendedor { get; set; }

    public decimal Iepm { get; set; }

    public string Valoracion { get; set; } = null!;

    public DateTime? FechaCalculo { get; set; }

    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;

    public virtual EncuestasIepm IdEncuestaNavigation { get; set; } = null!;

    public virtual ICollection<ResultadosAccionesIepm> ResultadosAccionesIepms { get; set; } = new List<ResultadosAccionesIepm>();
}
