using System.ComponentModel.DataAnnotations;

namespace Diplom.Domain.ViewModels.Authorization
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }

}
