using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_Learning_Hub_Hotel.Models;

public partial class PUser
{
    public decimal UserId { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public int? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    public virtual ICollection<PReservation> PReservations { get; set; } = new List<PReservation>();

    public virtual ICollection<PTestimonial> PTestimonials { get; set; } = new List<PTestimonial>();

    public virtual ICollection<PUserlogin> PUserlogins { get; set; } = new List<PUserlogin>();
}
