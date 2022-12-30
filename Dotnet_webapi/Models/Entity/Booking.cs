using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class Booking
{
    public int BookingId { get; set; }

    public string BookingRef { get; set; }

    public string BookingName { get; set; }

    public int? AccountId { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public DateTime? UpdateTs { get; set; }

    public decimal? Price { get; set; }

    public virtual Account Account { get; set; }

    public virtual ICollection<BookingLeg> BookingLegs { get; } = new List<BookingLeg>();

    public virtual ICollection<Passenger> Passengers { get; } = new List<Passenger>();
}
