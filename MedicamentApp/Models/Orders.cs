using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicamentApp.Models
{
    public class Orders
    {
        [Key]
        public int Идентификатор { get; set; }

        [ForeignKey("User")]
        public int Код_пользователя { get; set; }
        public Users User { get; set; }

        [ForeignKey("Drug")]
        public int Идентификатор_лекарства { get; set; }
        public Drug Drug { get; set; }

        [Required]
        public DateTime Дата_заказа { get; set; }

        [Required]
        public int Количество { get; set; }
    }
}




