using CreatorFund.Application.Data;
using CreatorFund.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace CreatorFund.Application.Services;

public class DemoService(CreatorFundDbContext dbContext)
{
    public async Task<List<Demo>> GetAllAsync()
    {
        return await dbContext.Set<Demo>().ToListAsync();
    }

    // Method to add a new Demo record
    public async Task AddAsync(Demo demo)
    {
        await dbContext.Set<Demo>().AddAsync(demo);
        await dbContext.SaveChangesAsync();
    }
}
