using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SqlServer.Entities;
using SqlServer.Interfaces;

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
    }
}
