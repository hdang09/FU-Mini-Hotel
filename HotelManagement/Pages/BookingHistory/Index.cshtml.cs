using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace HotelManagement.Pages.BookingHistory
{
    public class IndexModel : PageModel
    {
        private readonly IRoomService _service;
        public List<BookingHistoryDTO> Bookings { get; set; }

        public IndexModel(IRoomService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            Bookings = _service.GetBookingByCusId(3);
        }
    }
}
