using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NoteApp.Services;

public class EmailService
{
    private readonly string _smtpServer = "smtp.gmail.com"; // Configure your SMTP server
    private readonly int _smtpPort = 587; // Port number (e.g., 587 for TLS)
    private readonly string _smtpUser = "m.mastali7@gmail.com"; // SMTP username
    private readonly string _smtpPass = "odjm vapt mgod dzgu"; // SMTP password

    public async Task SendConfirmationEmailAsync(string email, string token)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpUser),
            Subject = "Confirm your email",
            Body = $"Please confirm your email by clicking <a href='http://localhost:5299/Account/ConfirmEmail?email={email}&token={token}'>here</a>.",
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        using var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}