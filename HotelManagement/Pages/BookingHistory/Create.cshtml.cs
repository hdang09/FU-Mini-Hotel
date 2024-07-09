using BusinessObjects.DTO;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using Services;
using System.Text.Json;

namespace HotelManagement.Pages.BookingHistory
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public BookingHistoryDTO BookingHistory { get; set; }

        private readonly IBookingHistoryService _bookingService;
        private readonly IRoomService _roomService;
        public BookingDTO Booking { get; private set; }
        public Customer Customer { get; set; }
        public RoomDTO Room { get; set; }
        public BookingDetailsDTO BookingDetailsDTO { get; set; }

        public List<RoomInformation> Rooms { get; set; }

        public List<BookingDetailsDTO> BookingDetailsDTOs { get; set; }

        public List<RoomInformation> BookingRooms { get; set; }

        private const string SessionKeyBookingRooms = "BookingRooms";

        private readonly FuminiHotelManagementContext _db;

        public CreateModel(IBookingHistoryService bookingService, IRoomService roomService, FuminiHotelManagementContext db)
        {
            _bookingService = bookingService;
            _roomService = roomService;
            BookingDetailsDTOs = new();
            Room = new();
            BookingRooms = new();
            _db = db;
        }

        public void OnGet()
        {
            Rooms = _roomService.GetRooms().Where(room => room.RoomStatus != 0).ToList() ?? new List<RoomInformation>();
            BookingRooms = [];
            SaveBookingRoomsToSession();
        }

        public IActionResult OnPostAddBooking(int roomId)
        {
            Rooms = _roomService.GetRooms().Where(room => room.RoomStatus != 0).ToList() ?? new List<RoomInformation>();
            LoadBookingRoomsFromSession();

            var roomToAdd = Rooms.FirstOrDefault(room => room.RoomId == roomId);
            if (roomToAdd != null)
            {
                BookingRooms.Add(roomToAdd);
                SaveBookingRoomsToSession();
            }

            return Page();
        }

        private void LoadBookingRoomsFromSession()
        {
            var sessionData = HttpContext.Session.GetString(SessionKeyBookingRooms);
            if (!string.IsNullOrEmpty(sessionData))
            {
                BookingRooms = JsonSerializer.Deserialize<List<RoomInformation>>(sessionData) ?? new List<RoomInformation>();
            }
        }
        private void SaveBookingRoomsToSession()
        {
            var sessionData = JsonSerializer.Serialize(BookingRooms);
            HttpContext.Session.SetString(SessionKeyBookingRooms, sessionData);
        }

        public void OnPostDeleteBooking()
        {

        }

        public IActionResult OnPostSaveBooking()
        {
            var booking = new BookingReservation
            {
                BookingDate = DateOnly.FromDateTime(DateTime.Now),
                BookingDetails = new List<BookingDetail>(),
                BookingStatus = 0,
                CustomerId = 3, //user.userId,
                TotalPrice = 0,
            };

            LoadBookingRoomsFromSession();

            foreach (var room in BookingRooms)
            {
                RoomDTO roomDTO = new RoomDTO()
                {
                    RoomDetailDescription = room.RoomDetailDescription,
                    RoomId = room.RoomId,
                    RoomMaxCapacity = room.RoomMaxCapacity,
                    RoomNumber = room.RoomNumber,
                    RoomPricePerDay = room.RoomPricePerDay,
                    RoomStatus = "1",
                    RoomType = "1"
                };
                BookingDetailsDTOs.Add(new BookingDetailsDTO()
                {
                    StartDate = BookingDetailsDTO.StartDate,
                    EndDate = BookingDetailsDTO.EndDate,
                    ActualPrice = room.RoomPricePerDay * (BookingDetailsDTO.EndDate - BookingDetailsDTO.StartDate).Days,
                    Room = roomDTO,
                });
            }

            booking.TotalPrice = 100; // CalculateTotalPrice(Rooms, CheckInDate, CheckOutDate);

            if (booking == null)
            {
                return Page();
            }

            _bookingService.CreateBooking(new BookingDTO()
            {
                BookingDate = BookingDetailsDTO.StartDate,
                TotalPrice = booking.TotalPrice,
                CustomerId = 3,
                BookingDetails = BookingDetailsDTOs,
                BookingStatus = 1,
            });
            _db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
    