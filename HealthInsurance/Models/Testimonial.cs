using System;
using System.Collections.Generic;

namespace HealthInsurance.Models;

public partial class Testimonial
{
    public decimal Id { get; set; }

    public decimal? Userid { get; set; }

    public string? Content { get; set; }

    public decimal? Status { get; set; }

    public virtual User? User { get; set; }
}
