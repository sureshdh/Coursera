namespace Course_for_Student.Model
{
	public class Student
	{

		public int Id { get; set; }
		public string Name { get; set; }
			
		public string email { get; set; }
			
		public string phone { get; set; }
			
	public ICollection<Enrollents> Enrollentes { get; set; }
	
	}
}
