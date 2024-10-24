using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_Learning_Hub_Hotel.Models;

public partial class PRoom
{
    public decimal Roomid { get; set; }

    public decimal? Roomnumber { get; set; }

    public decimal? Roomcapacity { get; set; }

    public decimal? PricePerNight { get; set; }

    public string? Isavailable { get; set; }

    public decimal? Hotelid { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    public virtual PHotel? Hotel { get; set; }

    public virtual ICollection<PReservation> PReservations { get; set; } = new List<PReservation>();
}
