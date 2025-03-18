using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public interface IDifficultyRepository
{
    public Task<List<Difficulty>> GetDifficultyAsync();
}