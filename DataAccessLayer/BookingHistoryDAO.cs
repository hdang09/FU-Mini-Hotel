using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class BookingHistoryDAO
    {
        public static async Task<BookingReservation?> GetBookingById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return await db.BookingReservations.FirstOrDefaultAsync(b => b.Equals(id));
        }

        public static List<BookingReservation?> GetBookings()
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations.ToList();
        }

        public static async Task<List<BookingHistoryDTO>> GetBookingByCusId(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return await db.BookingDetails
                .Include(bd => bd.BookingReservation)
                    .ThenInclude(br => br.Customer)
                .Include(bd => bd.Room)
                .Where(bd => bd.BookingReservation.CustomerId == id)
                .Select(bd => new BookingHistoryDTO
                {
                    BookingReservationId = bd.BookingReservationId,
                    RoomId = bd.RoomId,
                    RoomNumber = bd.Room.RoomNumber,
                    StartDate = bd.StartDate,
                    EndDate = bd.EndDate,
                    ActualPrice = bd.ActualPrice,
                    BookingDate = bd.BookingReservation.BookingDate,
                    TotalPrice = bd.BookingReservation.TotalPrice,
                    CustomerId = bd.BookingReservation.CustomerId,
                    BookingStatus = bd.BookingReservation.BookingStatus
                })
                .ToListAsync();
        }

        public static BookingReservation CreateBooking(BookingDTO bookingDto)
        {
            using var db = new FuminiHotelManagementContext();

            var bookingReservation = new BookingReservation
            {
                BookingReservationId = db.BookingReservations.Count() + 10,
                BookingDate = DateOnly.FromDateTime((DateTime)bookingDto.BookingDate),
                CustomerId = bookingDto.CustomerId,
                BookingStatus = bookingDto.BookingStatus,
                BookingDetails = new List<BookingDetail>(),
                TotalPrice = bookingDto.TotalPrice,
            };

            var booking = db.BookingReservations.Add(bookingReservation);

            db.SaveChanges();

            foreach (var detailDto in bookingDto.BookingDetails)
            {
                var room = db.RoomInformations.Find(detailDto.Room.RoomId);

                room.RoomStatus = 0;
                db.SaveChanges();

                var detail = new BookingDetail
                {
                    BookingReservationId = booking.Entity.BookingReservationId,
                    RoomId = detailDto.Room.RoomId,
                    StartDate = DateOnly.FromDateTime(detailDto.StartDate),
                    EndDate = DateOnly.FromDateTime(detailDto.EndDate),
                    ActualPrice = detailDto.ActualPrice,
                    Room = room,
                };

                //db.BookingDetails.Add(detail);
                booking.Entity.BookingDetails.Add(detail);
            }

            db.BookingReservations.Update(booking.Entity);

            db.SaveChanges();

            return bookingReservation;
        }

        public static BookingDTO? GetBookingDTOById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations
                .Where(b => b.BookingReservationId == id)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Room)
                .Select(b => new BookingDTO
                {
                }).FirstOrDefault();
        }
    }
}
