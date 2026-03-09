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
	}
}
