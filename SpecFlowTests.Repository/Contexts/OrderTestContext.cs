using System.Collections.Generic;
using SqlServer.Entities;

namespace SpecFlowTests.Repository.Contexts
{
    public class OrderTestContext
    {
        public Order CreatedOrder { get; set; }

        public ICollection<Order> CreatedOrders { get; set; }

        public Order RequestedOrder { get; set; }

        public int OrderCount { get; set; }
    }
}
