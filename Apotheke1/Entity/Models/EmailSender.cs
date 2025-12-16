using Microsoft.AspNetCore.Identity.UI.Services;

namespace Apotheke1.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Для лабораторної — нічого не робимо
            return Task.CompletedTask;
        }
    }
}
