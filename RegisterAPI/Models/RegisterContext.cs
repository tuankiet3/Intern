using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RegisterAPI.Models;

public partial class RegisterContext : DbContext
{
    public RegisterContext(DbContextOptions<RegisterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Aspiration> Aspirations { get; set; }

    public virtual DbSet<HighSchool> HighSchools { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasOne(d => d.Province).WithMany(p => p.Students).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
