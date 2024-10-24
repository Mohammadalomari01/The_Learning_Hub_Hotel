using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_Learning_Hub_Hotel.Models;

public partial class PHomepagecontent
{
    public decimal Homepagecontent { get; set; }

    public string? Projectname { get; set; }

    public string? Pagename { get; set; }

    public string? ImagepathTop1 { get; set; }
    [NotMapped]
    public IFormFile ImageFileTop1 { get; set; }

    public string? ImagepathTop2 { get; set; }
    [NotMapped]
    public IFormFile ImageFileTop2 { get; set; }

    public string? WelcomeText { get; set; }

    public string? Footerlocation { get; set; }

    public decimal? Footerphonenumber { get; set; }

    public string? Footeremail { get; set; }

    public decimal? Userloginid { get; set; }

    public virtual PUserlogin? Userlogin { get; set; }
}
