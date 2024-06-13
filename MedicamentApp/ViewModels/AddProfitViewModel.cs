using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class AddProfitViewModel
    {
        
        public int Идентификатор { get; set; }

        [Required(ErrorMessage = "Идентификатор лекарства обязателен")]
        public int Идентификатор_лекарства { get; set; }

        [Required(ErrorMessage = "Дата поступления обязательна")]
        public DateTime Дата_поступления { get; set; }

        [Required(ErrorMessage = "Количество обязательно")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть больше нуля")]
        public int Количество { get; set; }

        [StringLength(255)]
        public string Поставщик { get; set; }

        [Required(ErrorMessage = "Цена закупки обязательна")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена закупки должна быть неотрицательной")]
        public decimal Цена_закупки { get; set; }
    }
}
