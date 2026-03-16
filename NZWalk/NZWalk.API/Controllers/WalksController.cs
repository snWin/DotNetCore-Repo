using AutoMapper;
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
		private readonly IMapper mapper;
		private readonly IWalkRepository walkRepository;

		public WalksController(IMapper mapper,IWalkRepository walkRepository)
		{
			this.mapper = mapper;
			this.walkRepository = walkRepository;
		}

		// Create Walk
		// POST: /api/walks
		// POST method needs a body that this method can accept as parameters.
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			// Map AddWalkRequestDto to Domain Model
			var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

			await walkRepository.CreateAsync(walkDomainModel);

			//Map Domain Model to DTO
			return Ok(mapper.Map<WalkDto>(walkDomainModel));
		}

	}
}
