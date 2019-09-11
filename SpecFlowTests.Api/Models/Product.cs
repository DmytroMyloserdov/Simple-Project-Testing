using System;
using System.Collections.Generic;

namespace SpecFlowTests.Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public DateTime ProductionDate { get; set; }

        public string Manufacturer { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
