using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalk.API.Data
{
	public class NZWalksAuthDbContext : IdentityDbContext
	{
		// DbContextOptions is passed to the Constructor, NZWalksAuthDbContext that passing options to "base".
		// This is used when we inject the dbContext in the program.cs file with our own "options"
		public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options):base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "6f224b12-3081-42ff-9e01-df42048154c9";
			var writerRoleId = "1b1c4cac-1065-4644-ac9d-1bac405b5f91";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Id=readerRoleId,
					ConcurrencyStamp=readerRoleId,
					Name="Reader",
					NormalizedName="Reader".ToUpper()
				},
				new IdentityRole
				{
					Id=writerRoleId,
					ConcurrencyStamp=writerRoleId,
					Name="Writer",
					NormalizedName="Writer".ToUpper()
				}
			};
			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
