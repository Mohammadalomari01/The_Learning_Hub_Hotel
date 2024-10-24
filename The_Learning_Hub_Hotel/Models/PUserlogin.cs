using System;
using System.Collections.Generic;

namespace The_Learning_Hub_Hotel.Models;

public partial class PUserlogin
{
    public decimal Userloginid { get; set; }

    public string? Username { get; set; }

    public string? Passwordd { get; set; }

    public decimal? Roleid { get; set; }

    public decimal? UserId { get; set; }

    public virtual ICollection<PAboutpagecontent> PAboutpagecontents { get; set; } = new List<PAboutpagecontent>();

    public virtual ICollection<PBookingpagecontent> PBookingpagecontents { get; set; } = new List<PBookingpagecontent>();

    public virtual ICollection<PContactpagecontent> PContactpagecontents { get; set; } = new List<PContactpagecontent>();

    public virtual ICollection<PHomepagecontent> PHomepagecontents { get; set; } = new List<PHomepagecontent>();

    public virtual ICollection<PHotelspagecontent> PHotelspagecontents { get; set; } = new List<PHotelspagecontent>();

    public virtual ICollection<PRoomspagecontent> PRoomspagecontents { get; set; } = new List<PRoomspagecontent>();

    public virtual ICollection<PServicespagecontent> PServicespagecontents { get; set; } = new List<PServicespagecontent>();

    public virtual ICollection<PTeampagecontent> PTeampagecontents { get; set; } = new List<PTeampagecontent>();

    public virtual ICollection<PTestimonialpagecontent> PTestimonialpagecontents { get; set; } = new List<PTestimonialpagecontent>();

    public virtual PRole? Role { get; set; }

    public virtual PUser? User { get; set; }
}
