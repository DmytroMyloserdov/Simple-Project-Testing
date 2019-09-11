using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecFlowTests.Repository.Contexts;
using SqlServer.Entities;
using SqlServer.Interfaces;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Repository.Features.ProductFeature
{
    [Binding]
    public class ProductRepositoryTestSteps
    {
        private ProductTestContext Context { get; set; }

        private readonly IProductRepository _productRepository;

        private readonly Fixture _fixture;

        public ProductRepositoryTestSteps(ProductTestContext context, IProductRepository productRepository, Fixture fixture)
        {
            Context = context;
            _productRepository = productRepository;
            _fixture = fixture;
        }

        [Given(@"I know how many Products there are")]
        public void GivenIKnowHowManyProductsThereAre()
        {
            Context.ProductsCount = _productRepository.GetAll().Count();
        }
        
        [Given(@"(.*) Product created")]
        public void GivenProductCreated(int count)
        {
            SaveProducts(count);
        }
        
        [When(@"I add (.*) Product")]
        public void WhenIAddProduct(int count)
        {
            SaveProducts(count);
        }
        
        [When(@"I get Product by right Id")]
        public void WhenIGetProductByRightId()
        {
            Context.RequestedProduct = _productRepository.GetById(Context.CreatedProduct.Id);
        }
        
        [When(@"I get Product with wrong Id")]
        public void WhenIGetProductWithWrongId()
        {
            Context.RequestedProduct = _productRepository.GetById(-1);
        }
        
        [When(@"I get Products by ids")]
        public void WhenIGetProductsByIds()
        {
            Context.RequestedProducts = _productRepository.GetByIds(Context.CreatedProducts.Select(s => s.Id));
        }
        
        [When(@"I update Product")]
        public void WhenIUpdateProduct()
        {
            _productRepository.Update(new Product()
            {
                Id = Context.CreatedProduct.Id,
                Orders = Context.CreatedProduct.Orders,
                ProductionDate = DateTime.Now,
                Description = "Updated",
                Manufacturer = "Updated",
                Name = "Updated",
                Price = -1
            });
            _productRepository.SaveChanges();
        }
        
        [When(@"I delete Product with wrong Id")]
        public void WhenIDeleteProductWithWrongId()
        {
            _productRepository.Delete(-1);
            _productRepository.SaveChanges();
        }
        
        [When(@"I delete Product by right Id")]
        public void WhenIDeleteProductByRightId()
        {
            _productRepository.Delete(Context.CreatedProduct.Id);
            _productRepository.SaveChanges();
        }
        
        [Then(@"(.*) additional Product created")]
        public void ThenAdditionalProductCreated(int count)
        {
            Assert.AreEqual(Context.ProductsCount + count, _productRepository.GetAll().Count());
        }
        
        [Then(@"created Product returned")]
        public void ThenCreatedProductReturned()
        {
            Assert.AreSame(Context.CreatedProduct, Context.RequestedProduct);
        }
        
        [Then(@"no Product returned")]
        public void ThenNoProductReturned()
        {
            Assert.IsNull(Context.RequestedProduct);
        }
        
        [Then(@"created Products returned")]
        public void ThenCreatedProductsReturned()
        {
            Assert.IsTrue(AreEqualLists(Context.CreatedProducts, Context.RequestedProducts));
        }

        [Then(@"updated Product returned")]
        public void ThenUpdatedProductReturned()
        {
            Assert.AreEqual(Context.CreatedProduct.Id, Context.RequestedProduct.Id);
            Assert.IsTrue(Context.RequestedProduct.Description == "Updated");
        }

        private void SaveProducts(int count)
        {
            var products = CreateProducts(count);
            products.ForEach(p => _productRepository.Add(p));
            _productRepository.SaveChanges();

            if (count == 1)
            {
                Context.CreatedProduct = products.FirstOrDefault();
            }
            else
            {
                Context.CreatedProducts = products;
            }
        }

        private List<Product> CreateProducts(int count) =>
            _fixture.Build<Product>().Without(e => e.Id).Without(e => e.Orders).CreateMany(count).ToList();

        private bool AreEqualLists(IEnumerable<Product> left, IEnumerable<Product> right)
        {
            if (left == null && right == null)
            {
                return true;
            }

            if (left == null || right == null)
            {
                return false;
            }

            var firstNotSecond = left.Except(right).ToList();
            var secondNotFirst = right.Except(left).ToList();

            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }
    }
}
