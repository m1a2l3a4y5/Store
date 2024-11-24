using System;
using System.Collections.Generic;

namespace Store.Model;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<Korzina> Korzinas { get; set; } = new List<Korzina>();

    public virtual ICollection<Zakazi> Zakazis { get; set; } = new List<Zakazi>();
}
