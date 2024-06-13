using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class AddPharmacistsViewModel
    {
        public int Идентификатор { get; set; }
        [Required(ErrorMessage = "ФИО обязательно")]
        [StringLength(255, ErrorMessage = "ФИО не может превышать 255 символов")]
        public string ФИО { get; set; }

        [Required(ErrorMessage = "Дата приема обязательна")]
        [DataType(DataType.Date)]
        public DateTime Дата_приема { get; set; }

        [StringLength(50, ErrorMessage = "Статус не может превышать 50 символов")]
        public string Статус { get; set; }
    }
}
