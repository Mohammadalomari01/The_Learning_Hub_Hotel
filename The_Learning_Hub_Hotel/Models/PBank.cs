using System;
using System.Collections.Generic;

namespace The_Learning_Hub_Hotel.Models;

public partial class PBank
{
    public decimal Bankid { get; set; }

    public decimal? Creditcardnumber { get; set; }
    public decimal? Money { get; set; }

    public DateTime? Creditcardexp { get; set; }
}
