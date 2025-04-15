using System;

namespace Diplom.Domain
{
    public class Profile
    {
        public int Id { get; set; }

        public byte Age { get; set; }

        public string Address { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }
    }
}



