using System.Threading.Tasks;
using ScanMyTag.Models;

namespace ScanMyTag.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(EmailOptions emailOptions);
        Task SendEmailVerificationEmail(EmailOptions emailOptions);
    }
}