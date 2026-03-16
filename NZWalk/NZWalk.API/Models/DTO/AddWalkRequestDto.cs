namespace NZWalk.API.Models.DTO
{
	public class AddWalkRequestDto
	{
		// These values will be accepted from swagger
		public string Name { get; set; }
		public string Description { get; set; }
		public string LenghtInKm { get; set; }
		public string? WalkImageUrl { get; set; }
		public Guid DifficultyId { get; set; }
		public Guid RegionId { get; set; }
	}
}
