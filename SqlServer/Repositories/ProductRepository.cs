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

        public IEnumerable<Product> GetAll() => Products.ToList();

        public Product GetById(int id) => Products.Find(id);

        public void Add(Product item) => Products.Add(item);

        public void AddRange(IEnumerable<Product> items) => _context.Products.AddRange(items);

        public void Update(Product item)
        {
            var entity = Products.Find(item.Id);
            if (entity != null)
            {
                entity = item;
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

        public void DeleteRange(params int[] ids)
        {
            var entities = Products.Where(e => ids.Contains(e.Id)).ToList();
            if (entities.Any())
            {
                Products.RemoveRange(entities);
            }
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}
