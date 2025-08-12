using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RegisterAPI.Models;

public partial class Province
{
    [Key]
    public int ProvinceId { get; set; }

    public string? ProvinceName { get; set; }

    [InverseProperty("Province")]
    public virtual ICollection<HighSchool> HighSchools { get; set; } = new List<HighSchool>();

    [InverseProperty("Province")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
