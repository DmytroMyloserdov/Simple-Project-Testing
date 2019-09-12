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

        public IEnumerable<Product> GetAll()
        {
            lock (_context)
            {
                return Products.ToList();
            }
        }

        public Product GetById(int id)
        {
            lock (_context)
            {
                return Products.Find(id);
            }
        }

        public IEnumerable<Product> GetByIds(IEnumerable<int> ids)
        {
            lock (_context)
            {
                return Products.Where(s => ids.Contains(s.Id)).ToList();
            }
        }

        public void Add(Product item)
        {
            lock (_context)
            {
                Products.Add((item ?? throw new ArgumentNullException(nameof(item))));
            }
        }

        public void Update(Product item)
        {
            lock (_context)
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
        }

        public void Delete(int id)
        {
            lock (_context)
            {
                var entity = Products.Find(id);
                if (entity != null)
                {
                    Products.Remove(entity);
                }
            }
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}
