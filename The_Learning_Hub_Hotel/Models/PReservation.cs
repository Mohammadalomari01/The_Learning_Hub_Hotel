using System;
using System.Collections.Generic;

namespace The_Learning_Hub_Hotel.Models;

public partial class PReservation
{
    public decimal Reservationsid { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public decimal? Toltalprice { get; set; }

    public string? Invoicepdf { get; set; }

    public decimal? UserId { get; set; }

    public decimal? Roomid { get; set; }

    public virtual PRoom? Room { get; set; }

    public virtual PUser? User { get; set; }
}
