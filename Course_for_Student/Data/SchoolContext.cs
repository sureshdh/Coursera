using Course_for_Student.Model;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
	public DbSet<Student> Students { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<StudentCourse> StudentCourses { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Configure the many-to-many relationship
		modelBuilder.Entity<StudentCourse>()
			.HasKey(sc => new { sc.StudentId, sc.CourseId });

		modelBuilder.Entity<StudentCourse>()
			.HasOne(sc => sc.Student)
			.WithMany(s => s.StudentCourses)
			.HasForeignKey(sc => sc.StudentId);

		modelBuilder.Entity<StudentCourse>()
			.HasOne(sc => sc.Course)
			.WithMany(c => c.StudentCourses)
			.HasForeignKey(sc => sc.CourseId);
	}
}
