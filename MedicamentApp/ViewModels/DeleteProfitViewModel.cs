namespace MedicamentApp.ViewModels
{
    public class DeleteProfitViewModel
    {
        public int Идентификатор { get; set; }
        public int Идентификатор_лекарства { get; set; }
        public string Наименование_лекарства { get; set; }
        public DateTime Дата_поступления { get; set; }
        public int Количество { get; set; }
        public string Поставщик { get; set; }
        public decimal Цена_закупки { get; set; }
    }
}
