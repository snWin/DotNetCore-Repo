using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

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
			//var regions = dbContext.Regions.ToList();
			//we are sending the Domain Model back to our client, in this case Swagger
			//This is Coupling Domain Model and API view layer, which is not good practice.
			//We will create a separate class for API, which is called DTO (Data Transfer Object) and we will use that DTO to send/expose data back to client.
			//return Ok(regions); //return 200

			//The best practice - expose DTO instead of Domain Model. We have Separation Of Concern (SOC) as well
			//Get data from Database - Domain Model
			var regionsDomains = dbContext.Regions.ToList();

			//Map Domain Models to DTOs
			var regionsDto = new List<RegionDto>();
			foreach (var regionsDomain in regionsDomains)
			{
				regionsDto.Add(new RegionDto()
				{
					Id = regionsDomain.Id,
					Code = regionsDomain.Code,
					Name = regionsDomain.Name,
					RegionImageUrl = regionsDomain.RegionImageUrl
				});
			}
			//Return DTOs to client
			return Ok(regionsDto);

		}

		// Get single Region (Get Region by ID)
		// GET : https://localhost:portnumber/api/Regions/{id}
		[HttpGet]
		// adding DataType after column name, it is Type safe. -- {id:Guid}
		[Route("{id:Guid}")]
		public IActionResult GetById([FromRoute] Guid id)
		{
			//Access data from Domain Model and send back to client directly, which is not good practice.
			// var region = dbContext.Regions.Find(id);
			// Get Region Domain Model From Database
			//var region = dbContext.Regions.FirstOrDefault(x => x.Id == id); // x = such that
			//if (region == null)
			//{
			//	return NotFound();//404
			//}
			//return Ok(region);

			// Get Region Domain Model From Database
			var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id); // x = such that
			if (regionDomain == null)
			{
				return NotFound();//404
			}

			// Map/Convert Region Domain Model to Region DTO
			var regionDto = new RegionDto
			{
				Id = regionDomain.Id,
				Code = regionDomain.Code,
				Name = regionDomain.Name,
				RegionImageUrl = regionDomain.RegionImageUrl
			};

			// Return DTO back to client
			return Ok(regionDto);
		}

		//	POST to Create New Region
		// POST: https://localhost:portnumber/api/regions
		[HttpPost]
		public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			//Map or Convert DTO to Domain Model
			var regionDomainModel = new Region
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};

			// Use/Save Domain Model to create Region
			dbContext.Regions.Add(regionDomainModel);
			dbContext.SaveChanges();

			//should not return back Domain Model, we should return DTO back to client, which is best practice.
			//return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDomainModel);

			// Map Domain Model back to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			//return DTO back to client, which is best practice.
			// need to specify the Action name, which is GetById along with the route parameter, which is id, and the response body, regionDto.
			return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
		}

		// Update region
		// PUT: https://localhost:portnumber/api/regions/{id}
		// [FromBody] means we are accepting the data from request body, which is in JSON format, and we are converting that JSON data to UpdateRegionRequestDto object.
		[HttpPut]
		[Route("{id:guid}")]		
		public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto) 
		{
			// Check if region exists
			var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
			//EF Core is tracked by dbContext on the above line, so we can just update the Domain Model and call SaveChanges to save the changes to the database.

			if (regionDomainModel == null)
			{
				return NotFound(); // 404
			}

			// Map DTO to Domain Model - we accept DTO from swagger, so need to convert from DTO to Domain Model
			//Tracking on these below three properties, and changes have been done on these properties, so no need to call "Update" method, just call SaveChanges to save the changes to the database.
			regionDomainModel.Code = updateRegionRequestDto.Code;
			regionDomainModel.Name = updateRegionRequestDto.Name;
			regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;


			// use "SaveChanges", no need "Update" method. the changes in the Domain Model, so we just need to call SaveChanges to save the changes to the database.
			// because Entity Framework Core (EF Core) entities are tracked by the DbContext instance by default
			// This mechanism, known as "Change Tracking"
			dbContext.SaveChanges();

			// Convert Domain Model back to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			//send DTO back to client
			return Ok(regionDto);
		}

		// Delete Region
		// DELETE: https://localhost:portnumber/api/regions/{id}
		[HttpDelete]
		[Route("{id:Guid}")] // make Type safe by adding DataType after column name, which is Guid in this case.
		public IActionResult Delete([FromRoute] Guid id)
		{
			var regionDomainModel=	dbContext.Regions.FirstOrDefault(x=>x.Id == id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			dbContext.Regions.Remove(regionDomainModel);
			dbContext.SaveChanges();

			// return deleted Region back 
			// map Domain Model to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			//This will return deleted Region back to client, which is best practice. 200
			// if second time delete the deleted Region, it will return 404.
			return Ok(regionDto);
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
