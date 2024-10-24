using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_Learning_Hub_Hotel.Models;

public partial class PHotel
{
    public decimal Hotelid { get; set; }

    public string? Hotelname { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public virtual ICollection<PRoom> PRooms { get; set; } = new List<PRoom>();

    public virtual ICollection<PService> PServices { get; set; } = new List<PService>();

    public virtual ICollection<PTestimonial> PTestimonials { get; set; } = new List<PTestimonial>();
}
