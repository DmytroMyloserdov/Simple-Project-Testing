using SqlServer.Entities;
using System.Collections.Generic;

namespace SqlServer.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);

        void Add(Product item);

        void AddRange(IEnumerable<Product> items);

        void Update(Product item);

        void Delete(int id);

        void DeleteRange(params int[] ids);

        void SaveChanges();
    }
}
