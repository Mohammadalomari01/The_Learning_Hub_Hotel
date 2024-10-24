using System;
using System.Collections.Generic;

namespace The_Learning_Hub_Hotel.Models;

public partial class PTestimonial
{
    public decimal Testimonialid { get; set; }

    public string? TestimonialText { get; set; }

    public decimal? Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public decimal? UserId { get; set; }

    public decimal? Hotelid { get; set; }
    public string? Status { get; set; } = "Pending";

    public virtual PHotel? Hotel { get; set; }

    public virtual PUser? User { get; set; }
}
