using Course_for_Student.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StudentController : ControllerBase
	{
		private readonly AppDbContext _context;

		public StudentController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> AddStudent([FromBody] Student student)
		{
			_context.Students.Add(student);
			await _context.SaveChangesAsync();
			return Ok(student);
		}

		[HttpPost("{studentId}/courses")]
		public async Task<IActionResult> AssignCoursesToStudent(int studentId, [FromBody] List<int> courseIds)
		{
			var student = await _context.Students.FindAsync(studentId);
			if (student == null) return NotFound();

			foreach (var courseId in courseIds)
			{
				var course = await _context.Courses.FindAsync(courseId);
				if (course != null)
				{
					_context.StudentCourses.Add(new StudentCourse
					{
						StudentId = studentId,
						CourseId = courseId
					});
				}
			}

			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetStudents()
		{
			var students = await _context.Students
				.Include(s => s.StudentCourses)
				.ThenInclude(sc => sc.Course)
				.Select(s => new
				{
					s.Name,
					s.Email,
					s.Phone,
					CoursesEnrolled = string.Join(", ", s.StudentCourses.Select(sc => sc.Course.Name))
				})
				.ToListAsync();

			return Ok(students);
		}
	}
}
