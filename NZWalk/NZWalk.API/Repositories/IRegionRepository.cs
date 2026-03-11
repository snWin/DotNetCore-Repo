using System.Runtime.InteropServices;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
	public interface IRegionRepository
	{
		// Definition in Interface, for implementation in RegionRepository class
		Task<List<Region>> GetAllAsync(); 
		Task<Region?> GetByIdAsync(Guid id); //nullable value return

		 Task<Region> CreateAsync(Region region);

		Task<Region?> UpdateAsync(Guid id, Region region); // possible return null.

		Task<Region?> DeleteAsync(Guid id); // possible return null.
	}
}
