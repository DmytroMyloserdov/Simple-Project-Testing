using Refit;
using SpecFlowTests.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpecFlowTests.Api.RefitConfig
{
    public interface IProductsApi
    {
        [Get("/products/{id}")]
        Task<Product> GetById(int id);

        [Get("/products")]
        Task<IEnumerable<Product>> GetAll();

        [Post("/products")]
        Task<Product> Create([Body] Product product);

        [Put("/products/{id}")]
        Task<Product> Update(int id, [Body] Product product);

        [Delete("/products/{id}")]
        Task Delete(int id);
    }
}