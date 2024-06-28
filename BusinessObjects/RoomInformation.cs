    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class RoomInformation
{
    [Display(Name = "Room ID")]
    public int RoomId { get; set; }

    [Display(Name = "Room Number")]
    public string RoomNumber { get; set; } = null!;

    [Display(Name = "Description")]
    public string? RoomDetailDescription { get; set; }

    [Display(Name = "Maximum Capacity")]
    public int? RoomMaxCapacity { get; set; }

    [Display(Name = "Room Type ID")]
    public int RoomTypeId { get; set; }

    [Display(Name = "Room Status")]
    public byte? RoomStatus { get; set; }

    [Display(Name = "Price per Day")]
    public decimal? RoomPricePerDay { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual RoomType RoomType { get; set; } = null!;
}