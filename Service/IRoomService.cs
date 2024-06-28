using BusinessObjects;
using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IRoomService
    {
        List<RoomInformation> GetRooms();

        void CreateRoom(RoomInformation room);

        void DeleteRoom(int id);

        RoomInformation GetRoomById(int id);

        List<BookingHistoryDTO> GetBookingByCusId(int id);

        void UpdateRoom(RoomInformation room);

        List<BookingHistoryDTO> GetBooking();
    }
}
