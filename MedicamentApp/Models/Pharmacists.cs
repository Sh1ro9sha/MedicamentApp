using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.Models
{
    public class Pharmacists
    {
        [Key]
        public int Идентификатор { get; set; }

        [Required]
        [StringLength(255)]
        public string ФИО { get; set; }

        [Required]
        public DateTime Дата_приема { get; set; }

        [StringLength(50)]
        public string Статус { get; set; }
    }
}
