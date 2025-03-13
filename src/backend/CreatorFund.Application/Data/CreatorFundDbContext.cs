using CreatorFund.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorFund.Application.Data;

public class CreatorFundDbContext(DbContextOptions<CreatorFundDbContext> options) : DbContext(options)
{
    private DbSet<Demo> Demos => Set<Demo>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        DefineDemo(builder.Entity<Demo>());
    }

    private static void DefineDemo(EntityTypeBuilder<Demo> builder)
    {
        builder.ToTable("demos");

        builder.HasKey(ci => ci.Id);
        builder.Property(cb => cb.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
