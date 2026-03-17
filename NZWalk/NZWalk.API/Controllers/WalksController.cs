
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;

namespace NZWalk.API.Controllers
{
	// if you point /api/walks, you will reach this controller
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		/*private readonly IMapper mapper;*/
		private readonly IWalkRepository walkRepository;

		public WalksController(IWalkRepository walkRepository)  //IMapper mapper,
		{
			/* this.mapper = mapper; */
			this.walkRepository = walkRepository;
		}

		// Create Walk
		// POST: /api/walks
		// POST method needs a body that this method can accept as parameters.
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			// Map AddWalkRequestDto to Domain Model
			/* Auto Mapper
			  var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

			await walkRepository.CreateAsync(walkDomainModel);

			//Map Domain Model to DTO
			return Ok(mapper.Map<WalkDto>(walkDomainModel));
			*/

			var walkDomainModel = new Walk
			{
				Name = addWalkRequestDto.Name,
				Description = addWalkRequestDto.Description,
				LenghtInKm = addWalkRequestDto.LenghtInKm,
				WalkImageUrl = addWalkRequestDto.WalkImageUrl
			};

			walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

			var walkDto = new Walk
			{
				Name = walkDomainModel.Name,
				Description = walkDomainModel.Description,
				LenghtInKm = walkDomainModel.LenghtInKm,
				WalkImageUrl = walkDomainModel.WalkImageUrl
			};

			return Ok(walkDto);
		}

		// Get Walks
		// GET: /api/walks
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var walksDomainModel = await walkRepository.GetAllAsync();

			return Ok(walksDomainModel);

			//Map Domain Model to DTO
			/* return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));*/
		}

	}
}
