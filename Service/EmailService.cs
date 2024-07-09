using Services;
using System.Net.Mail;
using System.Net;

namespace Service
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string EMAIL = "smtp.housemate@gmail.com";
            string PASSWORD = "novf unlr pwot fsci";
            string SMTP_HOST = "smtp.gmail.com";
            int SMTP_PORT = 587;

            try
            {
                using (var client = new SmtpClient(SMTP_HOST, SMTP_PORT))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(EMAIL, PASSWORD);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("contact@hdang09.tech", "HAI DANG"),
                        Subject = subject,
                        Body = htmlMessage,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(email);

                    await client.SendMailAsync(mailMessage);
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to send email: {ex.Message}", ex);
            }
        }
    }
}
