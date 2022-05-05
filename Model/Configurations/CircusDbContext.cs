using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Configurations;

public class CircusDbContext : DbContext
{
    public DbSet<Circus> Circuses { get; set; }

    public CircusDbContext(DbContextOptions<CircusDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Circus>().HasIndex(n => n.Name).IsUnique();
    }
}