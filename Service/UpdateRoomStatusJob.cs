using BusinessObjects;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Quartz;
using Repository;

namespace Service.Quartzs
{
    public class UpdateRoomStatusJob : IJob
    {
        private readonly ILogger<UpdateRoomStatusJob> _logger;
        private readonly IRoomRepository _roomRepository;

        public UpdateRoomStatusJob(ILogger<UpdateRoomStatusJob> logger, IRoomRepository roomRepository)
        {
            _logger = logger;
            _roomRepository = roomRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var bookingDetails = _roomRepository.GetBookingByCusId(3);

                
                foreach (var bookingDetail in bookingDetails)
                {
                    _logger.LogWarning(bookingDetail.BookingDate.ToString());
                    _logger.LogWarning(bookingDetail.BookingStatus.ToString());
                    _logger.LogWarning((bookingDetail.BookingDate <= DateOnly.FromDateTime(DateTime.Now) && bookingDetail.BookingStatus == 0).ToString());
                    if (bookingDetail.BookingDate <= DateOnly.FromDateTime(DateTime.Now) && bookingDetail.BookingStatus == 0)
                    {
                        _logger.LogWarning(bookingDetail.ToString());
                        RoomInformation Room = _roomRepository.GetRooms().FirstOrDefault(r => r.RoomId == bookingDetail.RoomId);
                        Room.RoomStatus = 1;
                        _roomRepository.UpdateRoom(Room);
                        _logger.LogWarning("Update Room status...");
                        //await _hubContext.SendMessageWithData(_roomRepository.GetRooms().Where(r => r.RoomId != booking?.BookingDetails[0]?.Room?.RoomId && r.RoomStatus != 0));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error:{ex}");
            }
        }
    }
}
