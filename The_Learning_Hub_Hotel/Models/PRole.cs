using System;
using System.Collections.Generic;

namespace The_Learning_Hub_Hotel.Models;

public partial class PRole
{
    public decimal Roleid { get; set; }

    public string? Rolename { get; set; }

    public virtual ICollection<PUserlogin> PUserlogins { get; set; } = new List<PUserlogin>();
}
