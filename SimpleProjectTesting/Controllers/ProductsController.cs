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

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var entity = _productRepository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Product command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

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

            _productRepository.Update(command);
            _productRepository.SaveChanges();
            return Ok(command);
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (_productRepository.GetById(id) == null)
            {
                return NotFound();
            }

            _productRepository.Delete(id);
            _productRepository.SaveChanges();
            return Ok();
        }
    }
}
