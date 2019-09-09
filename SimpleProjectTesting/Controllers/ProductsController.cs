using SqlServer.Entities;
using SqlServer.Interfaces;
using System;
using System.Linq;
using System.Web.Http;

namespace SimpleProjectTesting.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IHttpActionResult GetAll() => Ok(_productRepository.GetAll());

        [HttpPost]
        public IHttpActionResult Post([FromBody] Product command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            command.ProductionDate = DateTime.Now;

            _productRepository.Add(command);
            _productRepository.SaveChanges();
            return Ok(command);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] Product command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (command.Orders != null && command.Orders.Any())
            {
                return BadRequest("You can't change orders");
            }

            if (_productRepository.GetById(command.Id) == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
