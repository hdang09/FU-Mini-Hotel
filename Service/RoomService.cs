using BusinessObjects;
using BusinessObjects.DTO;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository iRoomRepository;

        public RoomService()
        {
            iRoomRepository = new RoomRepository();
        }

        public void CreateRoom(RoomInformation room) => iRoomRepository.CreateRoom(room);

        public void DeleteRoom(int id) => iRoomRepository.DeleteRoom(id);

        public RoomInformation GetRoomById(int id) => iRoomRepository.GetRoomById(id);

        public List<RoomInformation> GetRooms() => iRoomRepository.GetRooms();

        public List<BookingHistoryDTO> GetBookingByCusId(int id) => iRoomRepository.GetBookingByCusId(id);

        public void UpdateRoom(RoomInformation room) => iRoomRepository.UpdateRoom(room);

        public List<BookingHistoryDTO> GetBooking() => iRoomRepository.GetBooking();
    }
}
