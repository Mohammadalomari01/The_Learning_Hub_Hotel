using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_Learning_Hub_Hotel.Models;

public partial class PTeam
{
    public decimal Teamid { get; set; }

    public string? TeamMembername { get; set; }

    public string? Position { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
}
