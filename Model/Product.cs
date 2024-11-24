using System;
using System.Collections.Generic;

namespace Store.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Opisanie { get; set; } = null!;

    public double Price { get; set; }

    public int CategoryId { get; set; }

    public int KolvoVnalichie { get; set; }

    public string? Image { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Korzina> Korzinas { get; set; } = new List<Korzina>();
}
