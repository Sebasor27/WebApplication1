namespace WebApplication1.Dto;
public class RespuestaDto
{
    public int IdRespuesta { get; set; }
    public int IdEncuesta { get; set; }
    public int IdPregunta { get; set; }
    public int Valor { get; set; }
    public string? Comentarios { get; set; }
    public int? IdEmprendedor { get; set; }
    public string? Pregunta { get; set; } 
}