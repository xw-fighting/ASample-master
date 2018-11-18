using System.Threading.Tasks;

namespace ASample.WebSite.Core.Infrastructure.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
