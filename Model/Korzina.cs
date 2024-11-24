using System;
using System.Collections.Generic;

namespace Store.Model;

public partial class Korzina
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Kolichestvo { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
