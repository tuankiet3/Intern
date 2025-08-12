using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RegisterAPI.Models;

public partial class Major
{
    [Key]
    public int MajorId { get; set; }

    public string? MajorName { get; set; }

    [InverseProperty("Major")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
