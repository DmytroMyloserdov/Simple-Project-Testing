using SqlServer.Entities;
using System.Collections.Generic;

namespace SqlServer.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);

        IEnumerable<Product> GetByIds(IEnumerable<int> ids);

        void Add(Product item);

        void Update(Product item);

        void Delete(int id);

        void SaveChanges();
    }
}
