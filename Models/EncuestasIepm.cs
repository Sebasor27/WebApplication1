using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class EncuestasIepm
{
    public int IdEncuestaIepm { get; set; }

    public int IdEmprendedor { get; set; }

    public DateTime? FechaAplicacion { get; set; }

    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;

    public virtual ICollection<RespuestasIepm> RespuestasIepms { get; set; } = new List<RespuestasIepm>();

    public virtual ICollection<ResultadosDimensionesIepm> ResultadosDimensionesIepms { get; set; } = new List<ResultadosDimensionesIepm>();

    public virtual ICollection<ResultadosIepm> ResultadosIepms { get; set; } = new List<ResultadosIepm>();

    public virtual ICollection<ResultadosIndicadoresIepm> ResultadosIndicadoresIepms { get; set; } = new List<ResultadosIndicadoresIepm>();
}
