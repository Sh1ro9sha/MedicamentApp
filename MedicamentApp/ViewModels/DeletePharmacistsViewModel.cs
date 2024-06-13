using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class DeletePharmacistsViewModel
    {
        public int Идентификатор { get; set; }

        public string ФИО { get; set; }

        public string Дата_приема { get; set; }

        public string Статус { get; set; }
    }
}
