using System.ComponentModel.DataAnnotations;
using Diplom.Domain.Entities;
using Diplom.Domain.Enum;

namespace Diplom.Domain.ViewModels.Good
{
    public class GoodViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите имя")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [MinLength(50, ErrorMessage = "Минимальная длина должна быть больше 50 символов")]
        public string Description { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Укажите стоимость")]
        public decimal Price { get; set; }

        [Display(Name = "Тип товара")]
        [Required(ErrorMessage = "Выберите типы")]
        public Tags Tag { get; set; }
        public Discount Discount { get; set; }

        public byte[]? Image { get; set; }
    }
}
