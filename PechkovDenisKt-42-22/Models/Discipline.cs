using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PechkovDenisKt_42_22.Models
{
    public class Discipline
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }

        public int LoadHours { get; set; } // Нагрузка в часах

        public virtual ICollection<Load> Loads { get; set; } = new List<Load>(); // Добавлено свойство для связи с нагрузками
    }

}
