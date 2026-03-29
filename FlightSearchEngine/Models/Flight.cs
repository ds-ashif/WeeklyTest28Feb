using System;
using System.Collections.Generic;

namespace FlightSearchEngine.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public string FlightName { get; set; } = null!;

    public string FlightType { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public decimal PricePerSeat { get; set; }
}
