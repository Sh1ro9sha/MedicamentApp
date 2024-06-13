using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.Models
{
    public class Role
    {
        [Key]
        public int Код_роли { get; set; }

        [Required]
        [StringLength(255)]
        public string Название { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
