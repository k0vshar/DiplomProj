using System;
using Diplom.Domain.Entities;

public class Basket
    {
        public long Id { get; set; }
        public Good? Good { get; set; }
        public int Count { get; set; }
        public long UserId { get; set; }
        public List<Order> Orders { get; set; }
        public User User { get; set; }
}
