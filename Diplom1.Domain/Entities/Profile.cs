using System;

namespace Diplom.Domain
{
    public class Profile
    {
        public int Id { get; set; }

        public byte Age { get; set; }

        public long UserId { get; set; }
        public string Address { get; set; } = "Пися";
        public User User { get; set; }
    }
}

