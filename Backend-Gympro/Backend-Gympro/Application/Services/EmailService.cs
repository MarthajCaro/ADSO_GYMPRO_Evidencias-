using MimeKit;
using MailKit.Net.Smtp;

namespace Backend_Gympro.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _senderEmail = "gymprobogota@gmail.com"; // Cambia con tu correo de Gmail
        private readonly string _senderPassword = "pnwbrkywejvrfeky"; // O la contraseña de aplicación si tienes 2FA habilitado
        //Contraseña del correo Gympro2025

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("No Reply", _senderEmail));
            message.To.Add(new MailboxAddress("Destinatario", toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body // Puedes personalizar el cuerpo en HTML si lo deseas
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, false);
                await client.AuthenticateAsync(_senderEmail, _senderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
