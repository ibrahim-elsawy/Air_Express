using System;
using System.Collections.Generic;

namespace Dotnet_webapi.Models.Entity;

public partial class Aircraft
{
    public string Model { get; set; }

    public decimal Range { get; set; }

    public int Class { get; set; }

    public decimal Velocity { get; set; }

    public string Code { get; set; }

    public virtual ICollection<Flight> Flights { get; } = new List<Flight>();
}
