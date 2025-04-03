namespace WebApplication1.Models
{
    public class EncuestaIceDto
    {
        public int IdEmprendedor { get; set; }
        public int ValorRespuesta { get; set; }
        public int IdPregunta { get; set; }
        public DateTime? FechaEvaluacion { get; set; }

        // Propiedad calculada que devuelve el valor ajustado según el tipo de pregunta
        public int ValorAjustado
        {
            get
            {
                // Lista de IDs de preguntas especiales (1-2-3 → 1-3-5)
                var preguntasEspeciales = new[] { 12, 21, 43, 52 };
                
                if (preguntasEspeciales.Contains(IdPregunta))
                {
                    // Conversión para preguntas especiales
                    return ValorRespuesta switch
                    {
                        1 => 1,
                        2 => 3,
                        3 => 5,
                        _ => ValorRespuesta // Por si acaso llega otro valor
                    };
                }
                return ValorRespuesta; // Preguntas normales (1-5)
            }
        }

        // Método estático para identificar si una pregunta es especial
        public static bool EsPreguntaEspecial(int idPregunta)
        {
            var preguntasEspeciales = new[] { 12, 21, 43, 52 };
            return preguntasEspeciales.Contains(idPregunta);
        }

        // Método estático para obtener los puntos máximos por competencia
        public static Dictionary<int, int> PuntosMaximosPorCompetencia = new Dictionary<int, int>
        {
            {1, 40},  // Comportamiento emprendedor-éxito (8 preguntas ×5)
            {2, 20},  // Creatividad (3 normales ×5 + 1 especial ×5)
            {3, 45},  // Liderazgo (8 normales ×5 + 1 especial ×5)
            {4, 15},  // Personalidad proactiva (3 ×5)
            {5, 35},  // Tolerancia a la incertidumbre (7 ×5)
            {6, 15},  // Capacidad de trabajo en equipo (3 ×5)
            {7, 45},  // Pensamiento estratégico (8 normales ×5 + 1 especial ×5)
            {8, 20},  // Proyección social (4 ×5)
            {9, 25},  // Orientación económica-financiera (4 normales ×5 + 1 especial ×5)
            {10, 20}  // Orientación a la tecnología e innovación (4 ×5)
        };

        // Método para validar valores de respuesta según el tipo de pregunta
        public bool EsValorValido()
        {
            if (EsPreguntaEspecial(IdPregunta))
            {
                return ValorRespuesta >= 1 && ValorRespuesta <= 3;
            }
            return ValorRespuesta >= 1 && ValorRespuesta <= 5;
        }
    }
}