using BusinessObjects;
using BusinessObjects.DTO;
using Repository;
using Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class BookingHistoryService : IBookingHistoryService
    {
        private readonly IBookingHistoryRepository _repo;
        private readonly IRoomRepository _roomRepository;
        private readonly BookingHub _hubContext;

        public BookingHistoryService(IBookingHistoryRepository repo, BookingHub hubContext, IRoomRepository roomRepository)
        {
            _repo = repo;
            _hubContext = hubContext;
            _roomRepository = roomRepository;
        }

        public async Task<BookingReservation?> GetBookingById(int id) => await _repo.GetBookingById(id);

        public async Task<List<BookingHistoryDTO>> GetBookingByCusId(int id) => await _repo.GetBookingByCusId(id);

        public async Task<BookingReservation> CreateBooking(BookingDTO booking)
        { 
            var data = _repo.CreateBooking(booking);
            await _hubContext.SendMessageWithData(_roomRepository.GetRooms().Where(r => r.RoomId != booking?.BookingDetails[0]?.Room?.RoomId && r.RoomStatus != 0));
            return data;
        }

        public List<BookingReservation?> GetBookings() => _repo.GetBookings();
    }
}
