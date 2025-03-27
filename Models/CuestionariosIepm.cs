using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class CuestionariosIepm
{
    public int IdCuestionario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Destinatario { get; set; } = null!;

    public string? Instrucciones { get; set; }

    public virtual ICollection<PreguntasIepm> PreguntasIepms { get; set; } = new List<PreguntasIepm>();
}
