using System;
using System.Collections.Generic;

namespace The_Learning_Hub_Hotel.Models;

public partial class PService
{
    public decimal Serviceid { get; set; }

    public string? Servicename { get; set; }

    public string? Servicetext { get; set; }

    public decimal? Hotelid { get; set; }

    public virtual PHotel? Hotel { get; set; }
}
