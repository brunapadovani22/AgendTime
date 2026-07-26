using System;
using System.Collections.Generic;
using System.Text;
using AgendTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendTime.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Clients");
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasIndex(c => c.Email)
                .IsUnique();

            entity.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(c => c.Notes)
                .HasMaxLength(1000);
        });

        base.OnModelCreating(modelBuilder);
    }
}