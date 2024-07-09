using Microsoft.AspNetCore.SignalR;

namespace Service
{
    public class BookingHub : Hub
    {
        private readonly IHubContext<BookingHub> _context;

        public BookingHub(IHubContext<BookingHub> context)
        {
            _context = context;
        }

        public async Task SendMessage(string message)
        {
            await _context.Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageWithData(object data)
        {
            await _context.Clients.All.SendAsync("ReceiveMessageWithData", data);
        }
    }
}
