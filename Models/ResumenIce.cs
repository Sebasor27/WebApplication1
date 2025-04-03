using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public partial class ResumenIce
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int IdResumenIce { get; set; }
    public int IdEmprendedor { get; set; }

    public decimal ValorIceTotal { get; set; }

    public int? IdIndicadores { get; set; }

    public int IdEncuesta { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaEvaluacion { get; set; } = DateTime.Now;


    public virtual Emprendedore IdEmprendedorNavigation { get; set; } = null!;

    public virtual Indicadore? IdIndicadoresNavigation { get; set; }
}
