using BusinessObjects;
using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRoomRepository
    {
        List<RoomInformation> GetRooms();

        void CreateRoom(RoomInformation room);

        void UpdateRoom(RoomInformation room);

        void DeleteRoom(int id);

        RoomInformation GetRoomById(int id);

        List<BookingHistoryDTO> GetBookingByCusId(int id);

        List<BookingHistoryDTO> GetBooking();

    }
}
