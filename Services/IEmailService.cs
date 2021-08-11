using KKTraders.Models;
using System.Threading.Tasks;

namespace KKTraders.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOption userEmailOptions);
        Task SendConfirmationMessage(UserEmailOption userEmailOptions);
        Task SendEmail(UserEmailOption options);
        Task SendMessageAboutStatusAccepted(OrderConfirmEmail user);
    }
}