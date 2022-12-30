using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class BoardingPass
{
    public int PassId { get; set; }

    public int? PassengerId { get; set; }

    public int? BookingLegId { get; set; }

    public string Seat { get; set; }

    public DateTime? BoardingTime { get; set; }

    public bool? Precheck { get; set; }

    public DateTime? UpdateTs { get; set; }

    public virtual BookingLeg BookingLeg { get; set; }

    public virtual Passenger Passenger { get; set; }
}
