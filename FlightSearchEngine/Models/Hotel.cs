using System;
using System.Collections.Generic;

namespace FlightSearchEngine.Models;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string HotelName { get; set; } = null!;

    public string HotelType { get; set; } = null!;

    public string Location { get; set; } = null!;

    public decimal PricePerDay { get; set; }
}
