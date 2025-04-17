using System;
using Diplom.Domain.Entities;

public class Order
    {
        public long Id { get; set; }

        public Good Good { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public long? BasketId { get; set; }
        public int Quantity { get; set; }

        public virtual Basket Basket { get; set; }
    }

