using System;
using Diplom.Domain.Entities;

public class Basket
    {
        public int Id { get; set; }
        public Good Good { get; set; }
        public int Count { get; set; }
        public User User { get; set; }
}


