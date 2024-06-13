using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class AddEmployeesViewModel
    {
        public int Идентификатор { get; set; }

        [Required(ErrorMessage = "ФИО обязательно")]
        [StringLength(255)]
        public string ФИО { get; set; }

        [StringLength(255, ErrorMessage = "Должность не может превышать 255 символов")]
        public string Должность { get; set; }

        [Required(ErrorMessage = "Код роли обязателен")]
        public int Код_роли { get; set; }
    }
}
