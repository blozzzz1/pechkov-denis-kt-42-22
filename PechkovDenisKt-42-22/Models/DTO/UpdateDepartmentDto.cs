﻿using System.ComponentModel.DataAnnotations;

namespace PechkovDenisKt_42_22.Models.DTO
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime FoundedDate { get; set; }

        public int? HeadId { get; set; }
    }
}
