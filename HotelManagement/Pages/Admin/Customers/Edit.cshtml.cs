using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Service;

namespace HotelManagement.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerService _service;

        public EditModel(ICustomerService service)
        {
            _service = service;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer =  _service.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_service.GetCustomerById(Customer.CustomerId) == null)
            {
                return NotFound();
            }

            try
            {
                _service.UpdateCustomer(Customer);
            }
            catch (Exception e)
            {
                
            }

            return RedirectToPage("Index");
        }
    }
}
