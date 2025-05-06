using System.ComponentModel.DataAnnotations;

namespace Diplom.Domain.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public long Id { get; set; }
        public byte Age { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
    }
}
