namespace PechkovDenisKt_42_22.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FoundationDate { get; set; }
        public int HeadId { get; set; } 
        public List<Teacher> Teachers { get; set; } 
    }
}
