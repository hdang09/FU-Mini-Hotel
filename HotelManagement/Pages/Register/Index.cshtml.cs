using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using Services;
using System.Net;
using System.Net.Mail;

namespace HotelManagement.Pages.Register
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Customer User { get; set; }

        private readonly ICustomerService _customerService;
        private readonly IEmailService _emailService;


        public IndexModel(ICustomerService customerService, IEmailService emailService)
        {
            _customerService = customerService;
            _emailService = emailService;
        }


        public async Task<IActionResult> OnPost()
        {
            try
            {
                _customerService.CreateCustomer(User);
                string emailBody = "<body style=\"font-family: Arial, sans-serif; background-color: #f7f7f7; padding: 20px;\">" +
                "          <div style=\"max-width: 600px; margin: 0 auto; background-color: #ffffff; border: 1px solid #dddddd; border-radius: 10px; overflow: hidden;\">" +
                "              <div style=\"background-color: #4CAF50; color: white; padding: 15px; text-align: center;\">" +
                "                  <h1 style=\"margin: 0; font-size: 24px;\">Welcome!</h1>" +
                "              </div>" +
                "              <div style=\"padding: 20px;\">" +
                "                  <p style=\"font-size: 16px; color: #333333;\">" +
                "                      Hi there," +
                "                  </p>" +
                "                  <p style=\"font-size: 16px; color: #333333;\">" +
                "                      Thank you for registering with us. Please click the button below to confirm your registration:" +
                "                  </p>" +
                "                  <div style=\"text-align: center; margin: 20px 0;\">" +
                "                      <a href=\"http://localhost:5197\" style=\"background-color: #4CAF50; color: white; padding: 15px 25px; text-decoration: none; border-radius: 5px; font-size: 16px;\">Confirm Registration</a>" +
                "                  </div>" +
                "                  <p style=\"font-size: 14px; color: #777777;\">" +
                "                      If the button above does not work, copy and paste the following link into your web browser:" +
                "                  </p>" +
                "                  <p style=\"font-size: 14px; color: #4CAF50;\">" +
                "                      <a href=\"http://localhost:5197\" style=\"color: #4CAF50; text-decoration: none;\">http://localhost:5197</a>" +
                "                  </p>" +
                "              </div>" +
                "              <div style=\"background-color: #f1f1f1; color: #666666; padding: 10px; text-align: center; font-size: 12px;\">" +
                "                  <p style=\"margin: 0;\">&copy; 2024 FUMiniHotel. All rights reserved.</p>" +
                "              </div>" +
                "          </div>" +
                "      </body>";
                await _emailService.SendEmailAsync(User.EmailAddress, "PRN221 | Registration Confirmation", emailBody);
                TempData["toast-success"] = "Register successfully";
                return RedirectToPage("/Index");
            }
            catch
            {
                TempData["toast-error"] = "Something went wrong";
            }

            return Page();
        }
    }
}
