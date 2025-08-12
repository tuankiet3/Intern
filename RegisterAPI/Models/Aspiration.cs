using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RegisterAPI.Models;

public partial class Aspiration
{
    [Key]
    public int AspirationId { get; set; }

    public string? AspirationName { get; set; }

    [InverseProperty("Aspiration")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
