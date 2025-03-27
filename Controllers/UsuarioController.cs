using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly CentroEmpContext _context;
    private readonly IConfiguration _configuration;

    public UsuarioController(CentroEmpContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.nombre) || string.IsNullOrEmpty(request.Contraseña))
        {
            return BadRequest("El nombre de usuario/correo y la contraseña son obligatorios.");
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u =>
                (u.Nombre == request.nombre || u.Correo == request.nombre) &&
                u.Contraseña == request.Contraseña);

        if (usuario == null)
        {
            return Unauthorized("Credenciales inválidas.");
        }

        if (usuario.TipoUsuario != "admin" && usuario.TipoUsuario != "reporteria")
        {
            return Unauthorized("Rol no válido.");
        }

        var token = GenerateJwtToken(usuario);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(Usuario usuario)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
        new Claim(ClaimTypes.Email, usuario.Correo),
        new Claim("TipoUsuario", usuario.TipoUsuario)
    };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    [HttpPost("recuperar-contrasena")]
    public async Task<IActionResult> RecuperarContrasena([FromBody] RecuperarContrasenaRequest request)
    {
        if (string.IsNullOrEmpty(request.Correo))
        {
            return BadRequest("El correo electrónico no puede ser nulo o vacío.");
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Correo == request.Correo);

        if (usuario == null)
        {
            return NotFound("Correo no registrado.");
        }

        if (string.IsNullOrEmpty(usuario.Correo))
        {
            return BadRequest("El correo electrónico del usuario no está configurado.");
        }

        var emailService = new EmailService(_configuration);
        var subject = "Recuperación de contraseña";
        var body = $"Su nombre de usuario es: {usuario.Nombre}\nSu contraseña es: {usuario.Contraseña}";

        emailService.SendEmail(usuario.Correo, subject, body);

        return Ok("Se ha enviado un correo con sus credenciales.");
    }

    [HttpGet("usuarios")]
    public async Task<IActionResult> GetUsuarios([FromQuery] string? tipoUsuario = null)
    {
        try
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(tipoUsuario))
            {
                query = query.Where(u => u.TipoUsuario == tipoUsuario);
            }

            var usuarios = await query
                .Select(u => new
                {
                    u.IdUsuario,
                    u.Nombre,
                    u.Correo,
                    u.TipoUsuario,
                    FechaRegistro = u.FechaCreacion.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    public class RecuperarContrasenaRequest
    {
        public required string Correo { get; set; }
    }
}

public class LoginRequest
{
    public string nombre { get; set; }
    public required string Contraseña { get; set; }
}


