using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsurance.Models;

public partial class Beneficiary
{
    public decimal Id { get; set; }

    public decimal? Subscriptionid { get; set; }

    public string? Name { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? Proofimage { get; set; }

    public decimal? Status { get; set; }

    public string? Relative { get; set; }

    public virtual Subscription? Subscription { get; set; }
}
