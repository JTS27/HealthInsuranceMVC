using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsurance.Models;

public partial class User
{
    public decimal Id { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public decimal? Roleid { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? Imagepath { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public decimal? Issub { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();
}
