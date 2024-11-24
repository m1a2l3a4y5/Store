using System;
using System.Collections.Generic;

namespace Store.Model;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Opisanie { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
