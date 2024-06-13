using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentApp.Models
{
    public class Drug
    {
        [Key]
        public int Идентификатор { get; set; }

        [Required]
        [StringLength(255)]
        public string Наименование { get; set; }

        [StringLength(1000)]
        public string Аннотация { get; set; }

        [Required]
        [ForeignKey("Manufacturers")]
        public int Идентификатор_производителя { get; set; }
        public Manufacturers Manufacturers { get; set; }

        [StringLength(50)]
        public string Единица_измерения { get; set; }

        [StringLength(255)]
        public string Место_хранения { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
