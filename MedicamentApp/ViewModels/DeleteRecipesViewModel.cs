using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class DeleteRecipesViewModel
    {
        public int Идентификатор { get; set; }

        public string Дата_назначения { get; set; }

        public string Наименование_лекарства { get; set; }

        public string ФИО_лечащего_врача { get; set; }

        public string Адрес_больницы { get; set; }
    }
}
