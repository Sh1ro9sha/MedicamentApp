namespace MedicamentApp.ViewModels
{
    public class DeleteExpensesViewModel
    {
        public int Идентификатор { get; set; }
        public int Идентификатор_лекарства { get; set; }
        public string Наименование_лекарства { get; set; }
        public DateTime Дата_реализации { get; set; }
        public int Количество { get; set; }
        public decimal Отпускная_цена { get; set; }
    }
}
