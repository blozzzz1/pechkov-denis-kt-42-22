using System.ComponentModel.DataAnnotations;

namespace PechkovDenisKt_42_22.Filters.TeacherFilters
{
    public class TeacherFilter
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? DegreeId { get; set; } 
        public int? PositionId { get; set; } 
        public int? DepartmentId { get; set; }
    }
}
