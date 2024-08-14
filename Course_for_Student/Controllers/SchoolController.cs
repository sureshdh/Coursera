using Course_for_Student.Model;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
	private readonly AppDbContext _context;

	public CoursesController(AppDbContext context)
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

	[HttpPost]
	public async Task<IActionResult> AddStudent([FromBody] Student student)
	{
		_context.Students.Add(student);
		await _context.SaveChangesAsync();
		return Ok(student);
	}

	[HttpPost("{studentId}/courses")]
	public async Task<IActionResult> AssignCourses(int studentId, [FromBody] List<int> courseIds)
	{
		var student = await _context.Students.FindAsync(studentId);
		if (student == null)
			return NotFound();

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
