using System;
using System.Collections.Generic;

namespace HealthInsurance.Models;

public partial class Bank
{
    public decimal Id { get; set; }

    public string? Username { get; set; }

    public decimal? Balance { get; set; }

    public decimal? Cardnumber { get; set; }

    public decimal? Cvv { get; set; }
}
