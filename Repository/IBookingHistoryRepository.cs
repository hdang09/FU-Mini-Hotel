using BusinessObjects;
using BusinessObjects.DTO;

namespace Repository
{
    public interface IBookingHistoryRepository
    {
        Task<BookingReservation?> GetBookingById(int id);
        Task<List<BookingHistoryDTO>> GetBookingByCusId(int id);
        BookingReservation CreateBooking(BookingDTO booking);

        List<BookingReservation?> GetBookings();
    }
}
