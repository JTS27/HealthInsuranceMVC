using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsurance.Models;

public partial class HomePage
{
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public decimal Id { get; set; }

    public string? Logo { get; set; }

    public string? Background { get; set; }

    public string? Txt1 { get; set; }

    public string? Txt2 { get; set; }

    public string? Txt3 { get; set; }

    public string? Item1 { get; set; }

    public string? Item2 { get; set; }

    public string? Item3 { get; set; }

    public string? Info1 { get; set; }

    public string? Info2 { get; set; }

    public string? Info3 { get; set; }

    public string? Img { get; set; }

    public string? Img1 { get; set; }

    public string? Img2 { get; set; }

    public string? Img3 { get; set; }

    public string? Footerabout { get; set; }

    public string? Footerlink { get; set; }

    public string? Footercontact { get; set; }
}
