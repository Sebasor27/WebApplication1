using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs
{
    public class EmprendedorDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La edad es obligatoria")]
        [StringLength(3, ErrorMessage = "La edad no puede exceder 3 caracteres")]
        public string Edad { get; set; } = null!;

        [Required(ErrorMessage = "El nivel de estudio es obligatorio")]
        [StringLength(50, ErrorMessage = "El nivel de estudio no puede exceder 50 caracteres")]
        public string NivelEstudio { get; set; } = null!;

        [Required(ErrorMessage = "Debe especificar si tiene trabajo en relación de dependencia")]
        public bool TrabajoRelacionDependencia { get; set; }

        [Required(ErrorMessage = "El sueldo mensual es obligatorio")]
        [StringLength(20, ErrorMessage = "El sueldo no puede exceder 20 caracteres")]
        public string SueldoMensual { get; set; } = null!;

        [Required(ErrorMessage = "El RUC es obligatorio")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El RUC debe tener 13 caracteres")]
        public string Ruc { get; set; } = null!;

        [Required(ErrorMessage = "Debe especificar el número de empleados hombres")]
        [Range(0, 1000, ErrorMessage = "El número de empleados hombres debe ser entre 0 y 1000")]
        public int EmpleadosHombres { get; set; }

        [Required(ErrorMessage = "Debe especificar el número de empleados mujeres")]
        [Range(0, 1000, ErrorMessage = "El número de empleados mujeres debe ser entre 0 y 1000")]
        public int EmpleadosMujeres { get; set; }

        [StringLength(50, ErrorMessage = "El rango de edad no puede exceder 50 caracteres")]
        public string? RangoEdadEmpleados { get; set; }

        [Required(ErrorMessage = "El tipo de empresa es obligatorio")]
        [StringLength(50, ErrorMessage = "El tipo de empresa no puede exceder 50 caracteres")]
        public string TipoEmpresa { get; set; } = null!;

        [Required(ErrorMessage = "El año de creación es obligatorio")]
        [Range(1900, 2100, ErrorMessage = "El año de creación debe ser entre 1900 y 2100")]
        public short AnoCreacionEmpresa { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200, ErrorMessage = "La dirección no puede exceder 200 caracteres")]
        public string Direccion { get; set; } = null!;

        [StringLength(10, ErrorMessage = "El teléfono no puede exceder 10 caracteres")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El celular es obligatorio")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El celular debe tener 10 caracteres")]
        public string Celular { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder 100 caracteres")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener 10 caracteres")]
        public string Cedula { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de registro es obligatoria")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        public bool Estado { get; set; } = true;

        public DateTime? FechaInactivacion { get; set; }
    }
}