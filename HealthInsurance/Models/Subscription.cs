using System;
using System.Collections.Generic;

namespace HealthInsurance.Models;

public partial class Subscription
{
    public decimal Id { get; set; }

    public DateTime? Subscriptiondate { get; set; }

    public decimal? Userid { get; set; }

    public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();

    public virtual User? User { get; set; }
}
