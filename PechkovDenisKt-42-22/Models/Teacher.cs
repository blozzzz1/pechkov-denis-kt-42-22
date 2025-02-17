namespace PechkovDenisKt_42_22.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DegreeId { get; set; } 
        public Degree Degree { get; set; } 
        public int PositionId { get; set; } 
        public Position Position { get; set; }
        public Department Departmentid { get; set; }
        public List<Discipline> Disciplines { get; set; } = new List<Discipline>(); 
    }
}