using System;
using SqlServer.Context;
using SqlServer.Entities;
using SqlServer.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SqlServer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SimpleContext _context;

        public ProductRepository(SimpleContext context)
        {
            _context = context;
        }

        private DbSet<Product> Products => _context.Set<Product>();

        public IEnumerable<Product> GetAll() => Products.Include(e => e.Orders).ToList();

        public Product GetById(int id) => Products.Include(e => e.Orders).FirstOrDefault(e => e.Id == id);

        public IEnumerable<Product> GetByIds(IEnumerable<int> ids) => Products.Where(s => ids.Contains(s.Id)).ToList();

        public void Add(Product item) => Products.Add((item ?? throw new ArgumentNullException(nameof(item))));

        public void Update(Product item)
        {
            var entity = Products.Find((item ?? throw new ArgumentNullException(nameof(item))).Id);
            if (entity != null)
            {
                entity.Description = item.Description;
                entity.Manufacturer = item.Manufacturer;
                entity.Name = item.Name;
                entity.Price = item.Price;
                entity.ProductionDate = item.ProductionDate;
            }
        }

        public void Delete(int id)
        {
            var entity = Products.Find(id);
            if (entity != null)
            {
                Products.Remove(entity);
            }
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}
