using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;


namespace NZWalk.API.Repositories
{
	public class SQLRegionRepository : IRegionRepository
	{
		private readonly NZWalksDbContext dbContext;

		public SQLRegionRepository(NZWalksDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Region> CreateAsync(Region region)
		{
			await dbContext.Regions.AddAsync(region);
			await dbContext.SaveChangesAsync();
			return region; // return newly created region back
		}
				
		public async Task<Region?> GetByIdAsync(Guid id)
		{
		 	return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<List<Region>> GetAllAsync()
		{
			// Implementation for Definition in IRegionRepository
			return await dbContext.Regions.ToListAsync();
		}

		public async Task<Region?> UpdateAsync(Guid id, Region region)
		{
			var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);

			if (existingRegion == null)
			{
				return null;
			}

			existingRegion.Code = region.Code;
			existingRegion.Name = region.Name;	
			existingRegion.RegionImageUrl = region.RegionImageUrl;

			await dbContext.SaveChangesAsync();
			return existingRegion;
		}

		public async Task<Region?> DeleteAsync(Guid id)
		{
			var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if(existingRegion == null)
			{
				return null;
			}

			dbContext.Regions.Remove(existingRegion); // Remove is not an async method, it just marks the entity for deletion
			await dbContext.SaveChangesAsync();
			return existingRegion;
		}
	}
}
