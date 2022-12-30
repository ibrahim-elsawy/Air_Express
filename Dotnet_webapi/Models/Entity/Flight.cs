using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class Flight
{
    public int FlightId { get; set; }

    public string FlightNo { get; set; }

    public DateTime ScheduledDeparture { get; set; }

    public DateTime ScheduledArrival { get; set; }

    public string DepartureAirport { get; set; }

    public string ArrivalAirport { get; set; }

    public string Status { get; set; }

    public string AircraftCode { get; set; }

    public DateTime? ActualDeparture { get; set; }

    public DateTime? ActualArrival { get; set; }

    public DateTime? UpdateTs { get; set; }

    public virtual Aircraft AircraftCodeNavigation { get; set; }

    public virtual ICollection<BookingLeg> BookingLegs { get; } = new List<BookingLeg>();

    public virtual Airport DepartureAirportNavigation { get; set; }
}
