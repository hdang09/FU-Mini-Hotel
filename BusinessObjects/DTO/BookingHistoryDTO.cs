using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO;

public partial class BookingHistoryDTO
{
    [Display(Name = "Booking Reservation ID")]
    public int BookingReservationId { get; set; }

    [Display(Name = "Room ID")]
    public int RoomId { get; set; }

    [Display(Name = "Room Number")]
    public string RoomNumber { get; set; } = null!;

    [Display(Name = "Start Date")]
    public DateOnly StartDate { get; set; }

    [Display(Name = "End Date")]
    public DateOnly EndDate { get; set; }

    [Display(Name = "Actual Price")]
    public decimal? ActualPrice { get; set; }

    [Display(Name = "Booking Date")]
    public DateOnly? BookingDate { get; set; }

    [Display(Name = "Total Price")]
    public decimal? TotalPrice { get; set; }

    [Display(Name = "Customer ID")]
    public int CustomerId { get; set; }

    [Display(Name = "Booking Status")]
    public byte? BookingStatus { get; set; }
}