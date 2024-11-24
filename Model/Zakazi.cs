using System;
using System.Collections.Generic;

namespace Store.Model;

public partial class Zakazi
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime DateZakaza { get; set; }

    public string Status { get; set; } = null!;

    public string AdresDostavki { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
