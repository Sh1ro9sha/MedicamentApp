using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class DeleteOrdersViewModel
    {
        public int Идентификатор { get; set; }

        public string Пользователь { get; set; }

        public string Лекарство { get; set; }

        public DateTime Дата_заказа { get; set; }

        public int Количество { get; set; }
    }
}
