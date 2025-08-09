using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace Inventorium.Services
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Email logic is bypassed for now
            return Task.CompletedTask;
        }
    }
}
