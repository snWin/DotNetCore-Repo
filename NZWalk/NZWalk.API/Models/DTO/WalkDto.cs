namespace NZWalk.API.Models.DTO
{
	public class WalkDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string LenghtInKm { get; set; }
		public string? WalkImageUrl { get; set; }

		public Guid DifficultyId { get; set; }//This DifficultyId will relate to Domain Model of Difficulty.
		public Guid RegionId { get; set; }//This RegionId will relate to Domain Model of Region.	}
	}
}
