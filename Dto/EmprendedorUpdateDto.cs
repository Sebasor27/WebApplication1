using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs
{
    public class EmprendedorUpdateDto
    {
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string? Nombre { get; set; }

        [StringLength(3, ErrorMessage = "La edad no puede exceder 3 caracteres")]
        public string? Edad { get; set; }

        [StringLength(50, ErrorMessage = "El nivel de estudio no puede exceder 50 caracteres")]
        public string? NivelEstudio { get; set; }

        public bool? TrabajoRelacionDependencia { get; set; }

        [StringLength(20, ErrorMessage = "El sueldo no puede exceder 20 caracteres")]
        public string? SueldoMensual { get; set; }

        [Range(0, 1000, ErrorMessage = "El número de empleados hombres debe ser entre 0 y 1000")]
        public int? EmpleadosHombres { get; set; }

        [Range(0, 1000, ErrorMessage = "El número de empleados mujeres debe ser entre 0 y 1000")]
        public int? EmpleadosMujeres { get; set; }

        public string? Ruc { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener 10 caracteres")]
        public string? Cedula { get; set; }

        [StringLength(200, ErrorMessage = "La dirección no puede exceder 200 caracteres")]
        public string? Direccion { get; set; }

        [StringLength(10, ErrorMessage = "El teléfono no puede exceder 10 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "El celular debe tener 10 caracteres")]
        public string? Celular { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder 100 caracteres")]
        public string? Correo { get; set; }

        [StringLength(50, ErrorMessage = "El tipo de empresa no puede exceder 50 caracteres")]
        public string? TipoEmpresa { get; set; }

        [Range(1900, 2100, ErrorMessage = "El año de creación debe ser entre 1900 y 2100")]
        public short? AnoCreacionEmpresa { get; set; }

        [StringLength(50, ErrorMessage = "El rango de edad no puede exceder 50 caracteres")]
        public string? RangoEdadEmpleados { get; set; }
    }
}