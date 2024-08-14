using Course_for_Student.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StudentCourseAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CourseController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CourseController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> AddCourse([FromBody] Course course)
		{
			_context.Courses.Add(course);
			await _context.SaveChangesAsync();
			return Ok(course);
		}
	}
}
