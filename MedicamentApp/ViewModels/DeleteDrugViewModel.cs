using System;
using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    public class DeleteDrugViewModel
    {
        public int Идентификатор { get; set; }
        public string Наименование { get; set; }
        public string Аннотация { get; set; }
        public int Идентификатор_производителя { get; set; }
        public string Единица_измерения { get; set; }
        public string Место_хранения { get; set; }
    }
}
