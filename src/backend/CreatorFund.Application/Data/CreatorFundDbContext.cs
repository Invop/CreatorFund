using Microsoft.EntityFrameworkCore;

namespace CreatorFund.Application.Data;

public class CreatorFundDbContext(DbContextOptions<CreatorFundDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
}
