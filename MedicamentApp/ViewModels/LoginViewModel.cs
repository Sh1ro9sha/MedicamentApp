using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.ViewModels
{
    
    public class LoginViewModel
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}