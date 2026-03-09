using System.Security.Principal;

namespace NZWalk.API.Models.Domain
{
	public class Region
	{
		public Guid id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; } // not nullable type
		public string? RegionImageUrl { get; set; } //A nullable type string data type, with "?"
	}
}
