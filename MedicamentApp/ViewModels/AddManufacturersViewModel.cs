using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class AddManufacturersViewModel
    {
        [Required(ErrorMessage = "Идентификатор обязателен")]
        public int Идентификатор { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(255)]
        public string Название { get; set; }

        [StringLength(255)]
        public string Адрес { get; set; }

        [StringLength(20)]
        public string Контактный_телефон { get; set; }
    }
}
