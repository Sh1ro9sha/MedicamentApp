using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class AddClientsViewModel
    {
        public int Идентификатор { get; set; }
        [Required(ErrorMessage = "ФИО обязательно")]
        [StringLength(255)]
        public string ФИО { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна")]
        public DateTime Дата_рождения { get; set; }

        [StringLength(255, ErrorMessage = "Место проживания не может превышать 255 символов")]
        public string Место_проживания { get; set; }

        [StringLength(20, ErrorMessage = "СНИЛС не может превышать 20 символов")]
        public string СНИЛС { get; set; }

        [StringLength(20, ErrorMessage = "Полис не может превышать 20 символов")]
        public string Полис { get; set; }
    }
}
