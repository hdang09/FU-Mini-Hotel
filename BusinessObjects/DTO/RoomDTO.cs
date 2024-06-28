using BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class RoomDTO
    {
        [Display(Name = "Room ID")]
        public int RoomId { get; set; }

        [Display(Name = "Room Number")]
        public string? RoomNumber { get; set; }

        [Display(Name = "Description")]
        public string? RoomDetailDescription { get; set; }

        [Display(Name = "Max Capacity")]
        public int? RoomMaxCapacity { get; set; }

        [Display(Name = "Status")]
        public string? RoomStatus { get; set; }

        [Display(Name = "Price Per Day")]
        public decimal? RoomPricePerDay { get; set; }

        [Display(Name = "Room Type")]
        public string? RoomType { get; set; }
    }
}
