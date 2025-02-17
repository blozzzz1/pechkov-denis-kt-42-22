namespace PechkovDenisKt_42_22.Models
{
    public class Load
    {
        public int Id { get; set; }
        public Teacher Teacher { get; set; } // Преподаватель
        public Discipline Discipline { get; set; } 
        public int Hours { get; set; } // Нагрузка в часах
    }
}
