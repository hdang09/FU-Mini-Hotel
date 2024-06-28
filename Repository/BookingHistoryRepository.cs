using BusinessObjects;
using BusinessObjects.DTO;
using DataAccessObjects;

namespace Repository
{
    public class BookingHistoryRepository : IBookingHistoryRepository
    {
        public async Task<BookingReservation?> GetBookingById(int id) => await BookingHistoryDAO.GetBookingById(id);

        public async Task<List<BookingHistoryDTO>> GetBookingByCusId(int id) => await BookingHistoryDAO.GetBookingByCusId(id);

        public BookingReservation CreateBooking(BookingDTO booking) => BookingHistoryDAO.CreateBooking(booking);

        public List<BookingReservation?> GetBookings() => BookingHistoryDAO.GetBookings();
    }
}
