using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Controllers
{
	// if you point /api/walks, you will reach this controller
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		
		

		// Create Walk
		// POST: /api/walks
		// POST method needs a body that this method can accept as parameters.
		//[HttpPost]
		//public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		//{
		//	//	// Map DTO to Domain Model


		//	}

	}
}
