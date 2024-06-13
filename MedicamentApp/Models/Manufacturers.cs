using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.Models
{
    public class Manufacturers
    {
        [Key]
        public int Идентификатор { get; set; }

        [Required]
        [StringLength(255)]
        public string Название { get; set; }

        [StringLength(255)]
        public string Адрес { get; set; }

        [StringLength(20)]
        public string Контактный_телефон { get; set; }

        public ICollection<Drug> Drugs { get; set; }
    }
}
