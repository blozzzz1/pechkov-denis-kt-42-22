using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PechkovDenisKt_42_22.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int DegreeId { get; set; }

        [ForeignKey("DegreeId")]
        public virtual Degree Degree { get; set; }

        public int PositionId { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }

        public int? DepartmentId { get; set; } // Добавлено свойство для связи с кафедрой

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; } // Связь с кафедрой

        public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
        public virtual ICollection<Load> Loads { get; set; } = new List<Load>();
    }
}