namespace WebApplication1.Models.DTOs
{
    public class EmprendedorResponseDto
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
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Estado { get; set; } = true;
        public DateTime? FechaInactivacion { get; set; }


    }
}