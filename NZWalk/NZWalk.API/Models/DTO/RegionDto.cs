namespace NZWalk.API.Models.DTO
{
	public class RegionDto
	{
		//This Dto is subset of Domain Model
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; } 
		public string? RegionImageUrl { get; set; }
	}
}
