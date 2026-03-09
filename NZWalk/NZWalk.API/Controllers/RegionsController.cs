using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Controllers
{
	// https://localhost:portnumber/api/Regions  -- is point to this controller, RegionController
	[Route("api/[controller]")] //Route attribute is basically define the Route and whenever user enters, this Route points to controller.
	[ApiController] //This will tell this application that this Controller is API use and automatically validate the model state.
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;

		// As we injected DbContext by using DI, now we can use DbContext inside the Controller through Constructor Injection.
		// type ctor, press double Tab to create constructor faster.
		// select dbContext. press Ctrl + . to create and assign field dbContext
		public RegionsController(NZWalksDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		// Get All Regions
		// GET : https://localhost:portnumber/api/Regions -- this is REST FUll URL to get all the regions.
		[HttpGet]
		public IActionResult GetAll()
		{
			//Access to Regions table
			var regions = dbContext.Regions.ToList();
			return Ok(regions); //return 200
		}

		// Get single Region (Get Region by ID)
		// GET : https://localhost:portnumber/api/Regions/{id}
		[HttpGet]
		// adding DataType after column name, it is Type safe. -- {id:Guid}
		[Route("{id:Guid}")]
		public IActionResult GetById([FromRoute]Guid id)
		{
			// var region = dbContext.Regions.Find(id);

			var region =dbContext.Regions.FirstOrDefault(x=> x.id == id); // x = such that
			if (region == null)
			{
				return NotFound();//404
			}
			return Ok(region);
		}


		//public IActionResult GetAll()
		//{
		//	var regions = new List<Region>
		//	{
		//		new Region
		//		{
		//			id=Guid.NewGuid(),
		//			Name="Ackland Region",
		//			Code="AKL",
		//			RegionImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Auckland_CBD_from_Mt_Eden.jpg/2560px-Auckland_CBD_from_Mt_Eden.jpg"
		//		},
		//		new Region
		//		{
		//			id=Guid.NewGuid(),
		//			Name="Wellington Region",
		//			Code="WLG",
		//			RegionImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Wellington_CBD_from_Mt_Victoria.jpg/2560px-Wellington_CBD_from_Mt_Victoria.jpg"
		//		}
		//	};
		//return Ok(regions); //return 200
		//}

		//[HttpGet]
		//public IActionResult GetAllRegions()
		//{
		//	var regions = new List<Region>
		//	{
		//		new Region
		//		{
		//			id=Guid.NewGuid(),
		//			Name="Ackland Region11",
		//			Code="AKL11",
		//			RegionImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Auckland_CBD_from_Mt_Eden.jpg/2560px-Auckland_CBD_from_Mt_Eden.jpg"
		//		},
		//		new Region
		//		{
		//			id=Guid.NewGuid(),
		//			Name="Wellington Region22",
		//			Code="WLG22",
		//			RegionImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Wellington_CBD_from_Mt_Victoria.jpg/2560px-Wellington_CBD_from_Mt_Victoria.jpg"
		//		}
		//	};
		//	return Ok(regions); //return 200
		//}
	}
}
