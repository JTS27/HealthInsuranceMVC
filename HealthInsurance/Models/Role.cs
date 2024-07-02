using System;
using System.Collections.Generic;

namespace HealthInsurance.Models;

public partial class Role
{
    public decimal Id { get; set; }

    public string? Role1 { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
