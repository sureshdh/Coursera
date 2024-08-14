namespace Course_for_Student.Model
{
	public class Course
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ProfessorName { get; set; }
		public string Description { get; set; }
		public ICollection<StudentCourse> StudentCourses { get; set; }
	}
}
