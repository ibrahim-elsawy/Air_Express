using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class Airport
{
    public string AirportCode { get; set; }

    public string AirportName { get; set; }

    public string City { get; set; }

    public string AirportTz { get; set; }

    public string Continent { get; set; }

    public string IsoCountry { get; set; }

    public string IsoRegion { get; set; }

    public bool Intnl { get; set; }

    public DateTime? UpdateTs { get; set; }

    public virtual ICollection<Flight> Flights { get; } = new List<Flight>();
}
