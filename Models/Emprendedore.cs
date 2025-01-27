using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Emprendedore
{
    public int IdEmprendedor { get; set; }

    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Edad { get; set; } = null!;

    public string NivelEstudio { get; set; } = null!;

    public bool TrabajoRelacionDependencia { get; set; }

    public string SueldoMensual { get; set; } = null!;

    public string Ruc { get; set; } = null!;

    public int EmpleadosHombres { get; set; }

    public int EmpleadosMujeres { get; set; }

    public string? RangoEdadEmpleados { get; set; }

    public string TipoEmpresa { get; set; } = null!;

    public short AnoCreacionEmpresa { get; set; }

    public string Direccion { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Celular { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public virtual ICollection<DatosEmp> DatosEmps { get; set; } = new List<DatosEmp>();

    public virtual ICollection<EncuestasIce> EncuestasIces { get; set; } = new List<EncuestasIce>();

    public virtual ICollection<EncuestasIepm> EncuestasIepms { get; set; } = new List<EncuestasIepm>();

    public virtual Usuario? IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<ResultadosIce> ResultadosIces { get; set; } = new List<ResultadosIce>();

    public virtual ICollection<ResultadosIepm> ResultadosIepms { get; set; } = new List<ResultadosIepm>();

    public virtual ResumenIce? ResumenIce { get; set; }

    public virtual ResumenIepm? ResumenIepm { get; set; }
}
