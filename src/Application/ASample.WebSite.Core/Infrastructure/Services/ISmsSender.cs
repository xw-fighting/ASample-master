using System.Threading.Tasks;

namespace ASample.WebSite.Core.Infrastructure.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
