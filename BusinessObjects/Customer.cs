using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class Customer
{
    [Display(Name = "Customer ID")]
    public int CustomerId { get; set; }

    [Display(Name = "Full Name")]
    public string? CustomerFullName { get; set; }

    [Display(Name = "Telephone")]
    public string? Telephone { get; set; }

    [Display(Name = "Email Address")]
    public string EmailAddress { get; set; } = null!;

    [Display(Name = "Birthday")]
    public DateOnly? CustomerBirthday { get; set; }

    [Display(Name = "Status")]
    public byte? CustomerStatus { get; set; }

    [Display(Name = "Password")]
    public string? Password { get; set; }

    public virtual ICollection<BookingReservation> BookingReservations { get; set; } = new List<BookingReservation>();
}
