using BusinessObjects;
using BusinessObjects.DTO;

namespace Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
