using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public int BookingId { get; set; }

    public string BookingRef { get; set; }

    public int? PassengerNo { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int? AccountId { get; set; }

    public DateTime? UpdateTs { get; set; }

    public int? Age { get; set; }

    public virtual Account Account { get; set; }

    public virtual ICollection<BoardingPass> BoardingPasses { get; } = new List<BoardingPass>();

    public virtual Booking Booking { get; set; }
}
