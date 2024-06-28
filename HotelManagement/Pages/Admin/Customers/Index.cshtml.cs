using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Service;

namespace HotelManagement.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerService _service;

        public IndexModel(ICustomerService service)
        {
            _service = service;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public void OnGet()
        {
            Customer = _service.GetCustomers();
        }
    }
}
