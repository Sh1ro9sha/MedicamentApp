using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentApp.Models
{
    public class Recipes
    {
        [Key]
        public int Идентификатор { get; set; }

        [Required]
        public DateTime Дата_назначения { get; set; }

        [ForeignKey("Drug")]
        public int Идентификатор_лекарства { get; set; }
        public Drug Drug { get; set; }

        [Required]
        [StringLength(255)]
        public string ФИО_лечащего_врача { get; set; }

        [StringLength(255)]
        public string Адрес_больницы { get; set; }
    }
}
