using System.Security.AccessControl;
using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;

namespace NZWalk.API.Controllers
{
	// https://localhost:portnumber/api/Regions  -- is point to this controller, RegionController
	[Route("api/[controller]")] //Route attribute is basically define the Route and whenever user enters, this Route points to controller.
	[ApiController] //This will tell this application that this Controller is API use and automatically validate the model state.
	public class RegionsController : ControllerBase
	{
		// First, We use dbContext to inject DataBase
		//private readonly NZWalksDbContext dbContext;

		// Second, we use Repository to inject DataBase, which is best practice, because it will separate the data access logic from the controller and it will make the code more maintainable and testable.
		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;

		// As we injected DbContext by using DI, now we can use DbContext inside the Controller through Constructor Injection.
		// type ctor, press double Tab to create constructor faster.
		// select dbContext. press Ctrl + . to create and assign field dbContext
		public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,IMapper mapper)
		{
			// First we use dbContext to inject DataBase, which is not good practice, because it will couple the controller with the data access logic and it will make the code less maintainable and testable.
			//this.dbContext = dbContext; // is not used. we use Repository instead.

			//Second, we use Repository to inject DataBase, which is best practice, because it will separate the data access logic from the controller and it will make the code more maintainable and testable.
			this.regionRepository = regionRepository;
			this.mapper = mapper;
		}

		// Get All Regions
		// GET : https://localhost:portnumber/api/Regions -- this is REST FUll URL to get all the regions.
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			//Access to Regions table
			//var regions = dbContext.Regions.ToList();
			//we are sending the Domain Model back to our client, in this case Swagger
			//This is Coupling Domain Model and API view layer, which is not good practice.
			//We will create a separate class for API, which is called DTO (Data Transfer Object) and we will use that DTO to send/expose data back to client.
			//return Ok(regions); //return 200

			//First - The best practice - expose DTO instead of Domain Model. We have Separation Of Concern (SOC) as well
			//Get data from Database - Domain Model
			//var regionsDomains = dbContext.Regions.ToList();  

			//Second use Repository instead of directly call on dbContext.
			var regionsDomains = await regionRepository.GetAllAsync();

			if (regionsDomains == null)
			{
				return NotFound();//404
			}

			//Map Domain Models to DTOs
			//To use the AutoMapper, register an AutoMapper profile in program.cs file - telling the application to use this Profile when the application starts.
			// will use Mapper instead of the Map Domain Models to DTOs
			/*
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

			return Ok(regionsDto);
			*/

			//Map Domain Models to DTOs
			//mapper.Map<Destination>(Source) - to map/convert from source to destination.
			//var regionsDto= mapper.Map<List<RegionDto>>(regionsDomains); 
			//return Ok(regionsDto);//Return DTOs to client

			// OR - the above two lines of code can be written in one line as below.

			return Ok(mapper.Map<List<RegionDto>>(regionsDomains)); //Map Domain Models to DTO and return DTOs to client by one line.
		}

		// GET action to retrieve the created item
		//[HttpGet("{id}", Name = nameof(GetByIdAsync))] // Named route for CreatedAtAction

		// Get single Region (Get Region by ID)
		// GET : https://localhost:portnumber/api/Regions/{id}
		[HttpGet]
		// adding DataType after column name, it is Type safe. -- {id:Guid}
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
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
			//This is standard (synchronous) version.
			//var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id); // x = such that

			// use Asynchronous but call directly on dbContext.
			// First - use directly on dbContext, now we will use Repository instead of directly call on dbContext.
			//var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
			// will call Repository instead of directly call on dbContext
			//var regionDomain = await regionRe

			// Second - use Repository instead of directly call on dbContext.
			var regionDomain = await regionRepository.GetByIdAsync(id);


			if (regionDomain == null)
			{
				return NotFound();//404
			}

			// Map/Convert Region Domain Model to Region DTO
			/* Use AutoMapper instead of the below code.
			var regionDto = new RegionDto
			{
				Id = regionDomain.Id,
				Code = regionDomain.Code,
				Name = regionDomain.Name,
				RegionImageUrl = regionDomain.RegionImageUrl
			};

			// Return DTO back to client
			return Ok(regionDto);
			*/

			//AutoMapper
			return Ok(mapper.Map<RegionDto>(regionDomain)); // Map/Convert Region Domain Model to Region DTO and Return DTO back to client by one line.
		}

		//	POST to Create New Region
		// POST: https://localhost:portnumber/api/regions
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			//Map or Convert DTO to Domain Model
			// ??? Question:  After CreatedAtAction, the result will return back to the caller, regionDomainModel
			// -- Answer : it will return "regionDto" beause GetById returns "regionDto"
			/* comment out the below code, because will use Mapper.*/
			var regionDomainModel = new Region
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};
			
			//Map/Convert DTO to Domain Model
			//var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);


			// Use/Save Domain Model to create Region
			//This is Standard (synchronous)
			//dbContext.Regions.Add(regionDomainModel);
			//dbContext.SaveChanges();

			// This is Asynchronous, which is best practice, because it will not block the thread and it will improve the performance of the application.
			// First - This is using dbContext directly, but we will use Repository instead of directly call on dbContext.
			//await dbContext.Regions.AddAsync(regionDomainModel);
			//await dbContext.SaveChangesAsync();

			// Second - using Repository instead of directly call on dbContext.
			regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

			//should not return back Domain Model, we should return DTO back to client, which is best practice.
			return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDomainModel);

			// Map Domain Model back to DTO
			/*comment out the below code, because will use Mapper.
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};
			*/
			//var regionDto =mapper.Map<RegionDto>(regionDomainModel);


			//return DTO back to client, which is best practice.
			// need to specify the Action name, which is GetById along with the route parameter, which is id, and the response body, regionDto.
			//return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
		}

		// Update region
		// PUT: https://localhost:portnumber/api/regions/{id}
		// [FromBody] means we are accepting the data from request body, which is in JSON format, and we are converting that JSON data to UpdateRegionRequestDto object.
		[HttpPut]
		[Route("{id:guid}")]		
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto) 
		{
			// Check if region exists
			//This is Standard (synchronous)
			//var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
			//EF Core is tracked by dbContext on the above line, so we can just update the Domain Model and call SaveChanges to save the changes to the database.

			//Asyncrhonous version using await
			// First - use directly on dbContext. need to chagen to call Repository instead of directly call on dbContext.
			//var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			//Second - use Repository instead of directly call on dbContext.
			//Map DTO to Domain Model
			var regionDomainModel = new Region
			{
				Code = updateRegionRequestDto.Code,
				Name = updateRegionRequestDto.Name,
				RegionImageUrl = updateRegionRequestDto.RegionImageUrl
			};

			regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
			 

			if (regionDomainModel == null)
			{
				return NotFound(); // 404
			}

			// Map DTO to Domain Model - we accept DTO from swagger, so need to convert from DTO to Domain Model
			//Tracking on these below three properties, and changes have been done on these properties, so no need to call "Update" method, just call SaveChanges to save the changes to the database.
			//regionDomainModel.Code = updateRegionRequestDto.Code;
			//regionDomainModel.Name = updateRegionRequestDto.Name;
			//regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;


			// use "SaveChanges", no need "Update" method. the changes in the Domain Model, so we just need to call SaveChanges to save the changes to the database.
			// because Entity Framework Core (EF Core) entities are tracked by the DbContext instance by default
			// This mechanism, known as "Change Tracking"
			//dbContext.SaveChanges(); // Standard (synchronous)
			// First - use directly on dbContext, but we will use Repository instead of directly call on dbContext.
			//await dbContext.SaveChangesAsync(); 

			
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
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			//starndar (synchronous)
			//var regionDomainModel=	dbContext.Regions.FirstOrDefault(x=>x.Id == id);
			// First - use directly on dbContext, but we will use Repository instead of directly call on dbContext.
			//var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			//Second - use Repository instead of directly call on dbContext.
			var regionDomainModel = await regionRepository.DeleteAsync(id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			//There is no Async for Remove() to delete data.
			// First - use directly on dbContext, but we will use Repository instead of directly call on dbContext.
			//dbContext.Regions.Remove(regionDomainModel);
			//dbContext.SaveChanges(); // starndard (synchronous)
			//await dbContext.SaveChangesAsync(); // using Asynchronous, which is best practice, because it will not block the thread and it will improve the performance of the application.

			// Second - use Repository instead of directly call on dbContext.
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
