using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentApp.Models
{
    public class Profit
    {
        [Key]
        public int Идентификатор { get; set; }

        [ForeignKey("Drug")]
        public int Идентификатор_лекарства { get; set; }
        public Drug Drug { get; set; }

        [Required]
        public DateTime Дата_поступления { get; set; }

        [Required]
        public int Количество { get; set; }

        [StringLength(255)]
        public string Поставщик { get; set; }

        [Required]
        public decimal Цена_закупки { get; set; }
    }
}
