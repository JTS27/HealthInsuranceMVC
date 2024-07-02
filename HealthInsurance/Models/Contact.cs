using System;
using System.Collections.Generic;

namespace HealthInsurance.Models;

public partial class Contact
{
    public decimal Id { get; set; }

    public string? Message { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }
}
