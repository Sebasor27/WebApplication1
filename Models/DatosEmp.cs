using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class DatosEmp
{
    public int IdDatosEmp { get; set; }

    public string NivelEdAlc { get; set; } = null!;

    public string RangoEdad { get; set; } = null!;

    public string NegociosEmp { get; set; } = null!;

    public string TitulosAlc { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public int IdEmprendedor { get; set; }

    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;
}
