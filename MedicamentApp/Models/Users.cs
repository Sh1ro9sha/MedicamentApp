using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentApp.Models
{
    public class Users
    {
        [Key]
        public int Код_пользователя { get; set; }

        [Required]
        [StringLength(255)]
        public string Логин { get; set; }

        [Required]
        [StringLength(255)]
        public string Пароль { get; set; }

        [ForeignKey("Role")]
        public int Код_роли { get; set; }
        public Role Role { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
