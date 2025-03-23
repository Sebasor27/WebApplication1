using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(string to, string subject, string body)
{
    // Validaciones
    if (string.IsNullOrEmpty(to))
    {
        throw new ArgumentException("La dirección de correo electrónico del destinatario no puede ser nula o vacía.", nameof(to));
    }

    if (string.IsNullOrEmpty(subject))
    {
        throw new ArgumentException("El asunto del correo electrónico no puede ser nulo o vacío.", nameof(subject));
    }

    if (string.IsNullOrEmpty(body))
    {
        throw new ArgumentException("El cuerpo del correo electrónico no puede ser nulo o vacío.", nameof(body));
    }

    var emailSettings = _configuration.GetSection("EmailSettings");

    var port = emailSettings["Port"];
    if (string.IsNullOrEmpty(port))
    {
        throw new InvalidOperationException("El puerto del servidor SMTP no está configurado en appsettings.json.");
    }

    var emailMessage = new MimeMessage();

    emailMessage.From.Add(new MailboxAddress("Soporte", emailSettings["Username"]));
    emailMessage.To.Add(new MailboxAddress("Usuario", to));
    emailMessage.Subject = subject;
    emailMessage.Body = new TextPart("plain") { Text = body };

    using (var client = new SmtpClient())
    {
        client.Connect(emailSettings["SmtpServer"], int.Parse(port), false);
        client.Authenticate(emailSettings["Username"], emailSettings["Password"]);
        client.Send(emailMessage);
        client.Disconnect(true);
    }
}
}