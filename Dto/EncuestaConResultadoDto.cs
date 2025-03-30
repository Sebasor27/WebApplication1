public class EncuestaConResultadoDto
{
    public int IdEncuesta { get; set; }
    public DateTime FechaAplicacion { get; set; }
    public int CantidadRespuestas { get; set; }
    public decimal? IepmTotal { get; set; }
    public string Valoracion { get; set; }
}