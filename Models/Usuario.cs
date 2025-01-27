using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string TipoUsuario { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Emprendedore> Emprendedores { get; set; } = new List<Emprendedore>();
}
