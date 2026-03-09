using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalk.API.Controllers
{
	//This is API Contrller
	//The Route is pointing to "api/[controller]", so the URL might be https://localhost:portnumber/api/Students
	//When the application run, the [controller] will be replaced by the name of the controller, which is "Students" in this case.
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		// Get : http://localhost:portnumber/api/Students
		[HttpGet]
		public IActionResult GetAllStudents()
		{
			string[] studentsName=new string[] { "John", "Jane", "Doe" };//assume come from DB
			return Ok(studentsName);
		}
	}
}
