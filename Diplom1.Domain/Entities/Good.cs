using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
using Diplom.Domain;
using Diplom.Domain.Enum;

namespace Diplom.Domain.Entities
{
    public class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Image { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Rate { get; set; }
        public Tags Tag { get; set; }
        public Discount? Discount { get; set; }
    }
}

