using BusinessObjects;
using BusinessObjects.DTO;

namespace Services
{
    public interface IBookingHistoryService
    {
        Task<BookingReservation?> GetBookingById(int id);
        Task<List<BookingHistoryDTO>> GetBookingByCusId(int id);
        Task<BookingReservation> CreateBooking(BookingDTO booking);

        List<BookingReservation?> GetBookings();
    }
}
