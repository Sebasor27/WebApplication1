public class DashboardEmprendedoresDto
{
    // Estadísticas generales
    public int TotalEmprendedores { get; set; }
    public int TotalEmpleados { get; set; }
    public decimal PromedioSueldos { get; set; }
    public decimal PromedioEdad { get; set; }

    // Filtros (valores vendrán de la base de datos)
    public List<string> OpcionesRangoEdad { get; set; }
    public List<string> OpcionesRangoSueldo { get; set; }
    public List<string> OpcionesNivelEstudio { get; set; }
    public List<string> OpcionesTipoEmpresa { get; set; }
    public List<int> OpcionesAnios { get; set; }

    // Gráficos (datos calculados desde la base de datos)
    public List<DistribucionDto> DistribucionEdades { get; set; }
    public List<DistribucionDto> DistribucionNivelEstudio { get; set; }
    public List<DistribucionDto> RelacionDependencia { get; set; }
    public List<DistribucionDto> DistribucionTipoEmpresa { get; set; }
    public List<DistribucionDto> DistribucionSueldos { get; set; }
    public List<DistribucionDto> EmpleadosPorGenero { get; set; }
    public List<EvolucionAnualDto> EvolucionAnual { get; set; }
}

public class DistribucionDto
{
    public string Name { get; set; }
    public int Value { get; set; }
}


public class EvolucionAnualDto
{
    public int Anio { get; set; }
    public int Emprendedores { get; set; }
    public int Empleados { get; set; }
    public decimal SueldoPromedio { get; set; }
    public decimal EdadPromedio { get; set; }
}