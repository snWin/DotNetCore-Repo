using Microsoft.EntityFrameworkCore;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Data
{
	public class NZWalksDbContext : DbContext
	{
		//To create Constructor faster, you can type "ctor" and then press double Tab.
		//In here, we want to pass the dbContextOptions in method parameter because we later want to send our own database connection to the Program.cs file.
		// We will then pass this dbContextOptions to the base class constructor of DbContext, so that it can be used by the DbContext.
		//we are basically passing the Options to the base class over here and to this DBContext class as well.
		//Options --> base --> DbContext
		public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
			//We will see the usage of this constructor in the Program.cs file when we create a new connection
			//and then injecte the connection through the Program.cs file.
			//This is how we will be configuring our database connection.
			

		}

		//DbSet is a property of the DbContext class. These DbSet property will create table inside database.
		//It represents a collection of entities that can be queried from the database.
		public DbSet<Difficulty> Difficulties { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Walk> Walks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Seed data for Difficulties
			// Easy, Medium, Hard

			var difficulties = new List<Difficulty>()
			{
				new Difficulty()
				{
					Id=Guid.Parse("6e8a8d57-46d7-4941-9ffd-e97ae65ba0c5"),
					Name="Easy"
				},
				new Difficulty()
				{
					Id=Guid.Parse("2b779f69-6f1b-4dd0-9736-192b98943f01"),
					Name="Medium"
				},
				new Difficulty()
				{
					Id=Guid.Parse("e0543ddb-eb25-45ca-ac0a-38890b54ed34"),
					Name="Hard"
				},
			};

			// seed difficulties to the database
			modelBuilder.Entity<Difficulty>().HasData(difficulties);


			// See data for Region
			var regions = new List<Region>
			{
				new Region
				{
					Id=Guid.Parse("77882740-f5a9-492c-b7b5-da94f52ecbfe"),
					Name="Ackland",
					Code="AKL",
					RegionImageUrl="https://www.freepik.com/free-photos-vectors/auckland"
				},
				new Region {
					Id=Guid.Parse("24999999-d924-48ba-9d12-780797723865"),
					Name="Northland",
					Code="NTL",
					RegionImageUrl=null
				},
			};
			// This will insert Region data into Region Table in Database
			// We have to run Entity Framework Core Migration
			// we use HasData dataMethod, and regions data.
			modelBuilder.Entity<Region>().HasData(regions);
		}

	}
}
