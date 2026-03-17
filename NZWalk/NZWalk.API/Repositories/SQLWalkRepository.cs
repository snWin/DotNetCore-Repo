using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
	/*A concrete class is a regular class that provides full implementations for all of its methods, 
	  whether they are self-defined, or inherited from an abstract class or interface. 
	  The key characteristic is that a concrete class has no unimplemented (abstract) methods, which means it can be instantiated with the new keyword to create an object. */

	// This concrete class will implement IWalkRepository 
	public class SQLWalkRepository : IWalkRepository
	{
		private readonly NZWalksDbContext dbContext;
		public SQLWalkRepository(NZWalksDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<Walk> CreateAsync(Walk walk)
		{
			await dbContext.Walks.AddAsync(walk);
			await dbContext.SaveChangesAsync();
			return walk;
		}

		public async Task<List<Walk>> GetAllAsync()
		{
			return await dbContext.Walks.ToListAsync();
		}
	}
}
