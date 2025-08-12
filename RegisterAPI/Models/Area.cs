using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RegisterAPI.Models;

public partial class Area
{
    [Key]
    public int AreaId { get; set; }

    public string? AreaName { get; set; }

    [InverseProperty("Area")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
