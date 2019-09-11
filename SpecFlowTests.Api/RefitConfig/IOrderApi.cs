using Refit;
using SpecFlowTests.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpecFlowTests.Api.RefitConfig
{
    public interface IOrderApi
    {
        [Get("/orders/{id}")]
        Task<Order> GetById(Guid id);

        [Get("/orders")]
        Task<IEnumerable<Order>> GetAll();

        [Post("/orders")]
        Task<Order> Create(IEnumerable<int> ids);
    }
}