using SqlServer.Entities;
using SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SimpleProjectTesting.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IHttpActionResult GetAll() => Ok(_orderRepository.GetAll());

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            var entity = _orderRepository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] IEnumerable<int> productIds)
        {
            var products = _productRepository.GetByIds(productIds).ToList();
            if (!products.Any())
            {
                return NotFound();
            }

            var order = new Order()
            {
                Date = DateTime.Now,
                Price = products.Select(s => s.Price).Sum(),
                Products = new List<Product>(products)
            };

            _orderRepository.Add(order);
            _orderRepository.SaveChanges();
            return Ok(order);
        }


    }
}
