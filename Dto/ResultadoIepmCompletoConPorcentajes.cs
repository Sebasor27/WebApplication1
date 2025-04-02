using WebApplication1.Models;

public class ResultadoIepmCompletoConPorcentajes
{
    public ResultadoIepmConPorcentaje IEPM { get; set; }
    public List<DimensionConPorcentaje> Dimensiones { get; set; }
    public List<IndicadorConPorcentaje> Indicadores { get; set; }
    public AccionesMejoraIepm AccionMejora { get; set; }
}

public class ResultadoIepmConPorcentaje : ResultadosIepm
{
    public double Porcentaje { get; set; } = 100; // El IEPM principal siempre es 100%
}

public class DimensionConPorcentaje : ResultadosDimensionesIepm
{
    public double Porcentaje { get; set; }
}

public class IndicadorConPorcentaje : ResultadosIndicadoresIepm
{
    public double Porcentaje { get; set; }
}