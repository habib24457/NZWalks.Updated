using System;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public interface IWalkRepository
	{
		Task <Walk>CreateAsync(Walk walk);
		Task<List<Walk>> GetWalk();
		Task<Walk?> GetById(Guid id);
		Task<Walk?> UpdateWalk(Guid id, Walk walk);
        Task<Walk?> DeleteWalk(Guid id);
    }
}

