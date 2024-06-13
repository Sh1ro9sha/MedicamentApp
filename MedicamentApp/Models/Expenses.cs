using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentApp.Models
{
    public class Expenses
    {
        [Key]
        public int Идентификатор { get; set; }

        [ForeignKey("Drug")]
        public int Идентификатор_лекарства { get; set; }
        public Drug Drug { get; set; }

        [Required]
        public DateTime Дата_реализации { get; set; }

        [Required]
        public int Количество { get; set; }

        [Required]
        public decimal Отпускная_цена { get; set; }
    }
}
