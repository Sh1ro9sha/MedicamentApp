using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class AddDrugViewModel
    {
        public int Идентификатор { get; set; } // Идентификатор лекарства

        [Required(ErrorMessage = "Наименование обязательно")]
        [StringLength(255, ErrorMessage = "Длина наименования не должна превышать 255 символов")]
        public string Наименование { get; set; }

        [StringLength(1000, ErrorMessage = "Длина аннотации не должна превышать 1000 символов")]
        public string Аннотация { get; set; }

        [Required(ErrorMessage = "Идентификатор производителя обязателен")]
        public int Идентификатор_производителя { get; set; }

        [StringLength(50, ErrorMessage = "Длина единицы измерения не должна превышать 50 символов")]
        public string Единица_измерения { get; set; }

        [StringLength(255, ErrorMessage = "Длина места хранения не должна превышать 255 символов")]
        public string Место_хранения { get; set; }
    }
}
