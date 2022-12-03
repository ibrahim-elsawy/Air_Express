using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class Account 
{
    public int AccountId { get; set; }

    public string Login { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int? FrequentFlyerId { get; set; }

    public DateTime? UpdateTs { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual FrequentFlyer FrequentFlyer { get; set; }

    public virtual ICollection<Passenger> Passengers { get; } = new List<Passenger>();

    public virtual ICollection<Phone> Phones { get; } = new List<Phone>();
}
