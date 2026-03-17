using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
	/* Whenever you have created new Interface and new Implementation, we need to inject this in Program.cs 
	   Otherwise, Controller wouldn't be able to get data
	 */
	public interface IWalkRepository
	{
		// Walk is Domain Model. Repository always deals with Domain Model.
		Task<Walk> CreateAsync(Walk walk);
		Task<List<Walk>> GetAllAsync();
	}
}
