using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;

namespace HotelManagement.Pages.Admin.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObjects.FuminiHotelManagementContext _context;

        public IndexModel(BusinessObjects.FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public IList<RoomInformation> RoomInformation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            RoomInformation = await _context.RoomInformations
                .Include(r => r.RoomType).ToListAsync();
        }
    }
}
