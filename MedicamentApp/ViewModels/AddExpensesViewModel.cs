using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class AddExpensesViewModel
    {
        [Required(ErrorMessage = "Идентификатор расхода обязателен")]
        public int Идентификатор { get; set; }

        [Required(ErrorMessage = "Идентификатор лекарства обязателен")]
        public int Идентификатор_лекарства { get; set; }

        [Required(ErrorMessage = "Дата реализации обязательна")]
        public DateTime Дата_реализации { get; set; }

        [Required(ErrorMessage = "Количество обязательно")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть больше нуля")]
        public int Количество { get; set; }

        [Required(ErrorMessage = "Отпускная цена обязательна")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Отпускная цена должна быть больше нуля")]
        public decimal Отпускная_цена { get; set; }
    }
}
