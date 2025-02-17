using System.ComponentModel.DataAnnotations;

namespace PechkovDenisKt_42_22.Models
{
    public class Degree
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // Например, "Кандидат наук", "Доктор наук"
    }
}