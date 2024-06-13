using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class AddOrdersViewModel
    {
        public int Идентификатор { get; set; }

        [Required(ErrorMessage = "Код пользователя обязателен")]
        public int Код_пользователя { get; set; }

        [Required(ErrorMessage = "Идентификатор лекарства обязателен")]
        public int Идентификатор_лекарства { get; set; }

        [Required(ErrorMessage = "Дата заказа обязательна")]
        public DateTime Дата_заказа { get; set; }

        [Required(ErrorMessage = "Количество обязательно")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть больше нуля")]
        public int Количество { get; set; }
    }
}
