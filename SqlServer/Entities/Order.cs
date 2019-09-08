using System;
using System.Collections.Generic;

namespace SqlServer.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public int Price { get; set; }

        public DateTime? Date { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
