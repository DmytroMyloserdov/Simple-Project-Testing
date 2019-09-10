using SqlServer.Context;
using SqlServer.Entities;
using SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SqlServer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SimpleContext _context;

        public OrderRepository(SimpleContext context)
        {
            _context = context;
        }

        private DbSet<Order> Orders => _context.Set<Order>();

        public Order GetById(Guid id) => Orders.Include(e => e.Products).FirstOrDefault(s => s.Id == id);

        public IEnumerable<Order> GetAll() => Orders.Include(e => e.Products).ToList();

        public void Add(Order item) => Orders.Add(item ?? throw new ArgumentNullException(nameof(item)));

        public void SaveChanges() => _context.SaveChanges();
    }
}
