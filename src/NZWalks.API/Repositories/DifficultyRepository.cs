using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class DifficultyRepository:IDifficultyRepository
{
    
    private readonly NZWalksDbContext dbContext;

    public DifficultyRepository(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<List<Difficulty>> GetDifficultyAsync()
    {
        return await dbContext.Diffculties.ToListAsync();
    }
}