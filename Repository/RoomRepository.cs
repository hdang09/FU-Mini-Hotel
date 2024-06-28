using BusinessObjects;
using BusinessObjects.DTO;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoomRepository : IRoomRepository
    {
        public void CreateRoom(RoomInformation room) => RoomDAO.SaveRoom(room);

        public void DeleteRoom(int id) => RoomDAO.DeleteRoom(id);

        public List<BookingHistoryDTO> GetBookingByCusId(int id) => RoomDAO.GetBookingByCusId(id);

        public RoomInformation GetRoomById(int id) => RoomDAO.GetRoomById(id);

        public List<RoomInformation> GetRooms() => RoomDAO.GetRooms();

        public void UpdateRoom(RoomInformation room) => RoomDAO.UpdateRoom(room);

        public List<BookingHistoryDTO> GetBooking() => RoomDAO.GetBooking();
    }
}
