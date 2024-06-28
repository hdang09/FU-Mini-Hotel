using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO
{
    public class BookingDetailsDTO
    {
        [Display(Name = "Room")]
        public RoomDTO Room { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Actual Price")]
        public decimal? ActualPrice { get; set; }

        // Calculate the actual price based on the room price per day and duration of stay
        public void CalculateActualPrice()
        {
            int days = (EndDate - StartDate).Days;
            ActualPrice = days * Room.RoomPricePerDay;
        }
    }
}
