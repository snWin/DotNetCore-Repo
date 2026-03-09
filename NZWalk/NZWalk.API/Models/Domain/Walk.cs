namespace NZWalk.API.Models.Domain
{
	public class Walk
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string LenghtInKm { get; set; }
		public string? WalkImageUrl { get; set; }
				
		public Guid DifficultyId { get; set; }//This DifficultyId will relate to Domain Model of Difficulty.
		public Guid RegionId { get; set; }//This RegionId will relate to Domain Model of Region.

		//Navigation property  This will allow us to access the Difficulty details when we have a Walk object. One-to-One relationship between Walk and Difficulty.
		public Difficulty Difficulty { get; set; }
		public Region Region { get; set; }

	}
}
