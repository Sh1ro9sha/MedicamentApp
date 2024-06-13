using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class AddRecipesViewModel
    {
        public int Идентификатор { get; set; }
        [Required(ErrorMessage = "Дата назначения обязательна")]
        public DateTime Дата_назначения { get; set; }

        [Required(ErrorMessage = "Идентификатор лекарства обязателен")]
        public int Идентификатор_лекарства { get; set; }

        [Required(ErrorMessage = "ФИО лечащего врача обязательно")]
        [StringLength(255)]
        public string ФИО_лечащего_врача { get; set; }

        [StringLength(255)]
        public string Адрес_больницы { get; set; }
    }
}
