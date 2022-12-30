using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class BookingLeg
{
    public int BookingLegId { get; set; }

    public int BookingId { get; set; }

    public int FlightId { get; set; }

    public int? LegNum { get; set; }

    public bool? IsReturning { get; set; }

    public DateTime? UpdateTs { get; set; }

    public virtual ICollection<BoardingPass> BoardingPasses { get; } = new List<BoardingPass>();

    public virtual Booking Booking { get; set; }

    public virtual Flight Flight { get; set; }
}
