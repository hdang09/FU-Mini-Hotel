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

namespace HotelManagement.Pages.Admin.Rooms
{
    public class EditModel : PageModel
    {
        private readonly BusinessObjects.FuminiHotelManagementContext _context;

        private readonly IRoomService _roomService;

        public EditModel(BusinessObjects.FuminiHotelManagementContext context, IRoomService roomService)
        {
            _context = context;
            _roomService = roomService;
        }

        [BindProperty]
        public RoomInformation RoomInformation { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roominformation = _roomService.GetRoomById(id);
            if (roominformation == null)
            {
                return NotFound();
            }
            RoomInformation = roominformation;

            ViewData["RoomTypeId"] = new SelectList(_context.RoomTypes, "RoomTypeId", "RoomTypeName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Attach(RoomInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomInformationExists(RoomInformation.RoomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoomInformationExists(int id)
        {
            return _context.RoomInformations.Any(e => e.RoomId == id);
        }
    }
}
