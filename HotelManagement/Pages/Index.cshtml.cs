using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace HotelManagement.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerService _service;

        
        public Customer User { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor, ICustomerService service)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _service = service;
        }

        public void OnGet()
        {

        }   

        public IActionResult OnPost()
        {
            // Admin role
            if (IsAdmin(User.EmailAddress, User.Password))
            {
                TempData["toast-success"] = "Welcome admin!";
                _httpContextAccessor.HttpContext.Session.SetString("Username", "Adminstrator");
                return RedirectToPage("/Admin/Customers/Index");
            }
            else
            {
                Customer? customer = _service.Login(User.EmailAddress, User.Password);

                if (customer == null)
                {
                    TempData["toast-error"] = "Account does not exist";
                    return Page();
                }
                
                if (customer.EmailAddress.Equals(User.EmailAddress) && customer.Password.Equals(User.Password))
                {
                    TempData["toast-success"] = $"Hi {customer.CustomerFullName}!";
                    _httpContextAccessor.HttpContext.Session.SetString("Username", customer.CustomerFullName);
                    return RedirectToPage("/BookingHistory/Index");
                }
                
                TempData["toast-error"] = "Invalid credentials";
                return Page();
            }
        }

        private bool IsAdmin(string email, string password)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();

            return email.Equals(configuration["AdminAccount:Email"]) && password.Equals(configuration["AdminAccount:Password"]);
        }
    }
}
