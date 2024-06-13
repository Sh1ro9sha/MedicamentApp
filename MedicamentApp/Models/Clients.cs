using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.Models
{
    public class Clients
    {
        [Key]
        public int Идентификатор { get; set; }

        [Required]
        [StringLength(255)]
        public string ФИО { get; set; }

        [Required]
        public DateTime Дата_рождения { get; set; }

        [StringLength(255)]
        public string Место_проживания { get; set; }

        [StringLength(20)]
        public string СНИЛС { get; set; }

        [StringLength(20)]
        public string Полис { get; set; }
    }
}
