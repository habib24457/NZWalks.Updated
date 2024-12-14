using System;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public interface IWalkRepository
	{
		Task <Walk>CreateWalkAsync(Walk walk);
		Task<List<Walk>> GetWalkAsync();
		Task<Walk?> GetWalkByIdAsync(Guid id);
		Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}

