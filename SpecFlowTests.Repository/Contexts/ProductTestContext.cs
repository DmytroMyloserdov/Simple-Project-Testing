using SqlServer.Entities;
using System.Collections.Generic;

namespace SpecFlowTests.Repository.Contexts
{
    public class ProductTestContext
    {
        public Product CreatedProduct { get; set; }

        public ICollection<Product> CreatedProducts { get; set; }

        public int ProductsCount { get; set; }

        public Product RequestedProduct { get; set; }

        public IEnumerable<Product> RequestedProducts { get; set; }

        public Product UpdatedProduct { get; set; }
    }
}
