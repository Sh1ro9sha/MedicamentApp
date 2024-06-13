using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentApp.Models
{
    public class Employees
    {
        [Key]
        public int Идентификатор { get; set; }

        [Required]
        [StringLength(255)]
        public string ФИО { get; set; }

        [StringLength(255)]
        public string Должность { get; set; }

        [ForeignKey("Role")]
        public int Код_роли { get; set; }
        public Role Role { get; set; }
    }
}
