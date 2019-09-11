using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecFlowTests.Repository.Contexts;
using SqlServer.Entities;
using SqlServer.Interfaces;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Repository.Features.OrderFeature
{
    [Binding]
    public class OrderRepositoryTestSteps
    {
        private OrderTestContext Context { get; set; }

        private readonly IOrderRepository _orderRepository;

        private readonly Fixture _fixture;

        public OrderRepositoryTestSteps(OrderTestContext context, IOrderRepository orderRepository, Fixture fixture)
        {
            Context = context;
            _orderRepository = orderRepository;
            _fixture = fixture;
        }

        [Given(@"I know how many Orders there are")]
        public void GivenIKnowHowManyOrdersThereAre()
        {
            Context.OrderCount = _orderRepository.GetAll().Count();
        }
        
        [Given(@"(.*) Order created")]
        public void GivenOrderCreated(int count)
        {
            SaveOrders(count);
        }
        
        [When(@"I add (.*) Order")]
        public void WhenIAddOrder(int count)
        {
            SaveOrders(count);
        }
        
        [When(@"I get Order by right Id")]
        public void WhenIGetOrderByRightId()
        {
            Context.RequestedOrder = _orderRepository.GetById(Context.CreatedOrder.Id);
        }
        
        [When(@"I get Order with wrong Id")]
        public void WhenIGetOrderWithWrongId()
        {
            Context.RequestedOrder = _orderRepository.GetById(Guid.Empty);
        }
        
        [Then(@"(.*) additional Order created")]
        public void ThenAdditionalOrderCreated(int count)
        {
            Assert.AreEqual(Context.OrderCount + count, _orderRepository.GetAll().Count());
        }
        
        [Then(@"created Order returned")]
        public void ThenCreatedOrderReturned()
        {
            Assert.AreSame(Context.CreatedOrder, Context.RequestedOrder);
        }
        
        [Then(@"no Order returned")]
        public void ThenNoOrderReturned()
        {
            Assert.IsNull(Context.RequestedOrder);
        }

        private void SaveOrders(int count)
        {
            var orders = CreateOrders(count, CreateProduct());
            orders.ForEach(o => _orderRepository.Add(o));
            _orderRepository.SaveChanges();

            if (count == 1)
            {
                Context.CreatedOrder = orders.FirstOrDefault();
            }
            else
            {
                Context.CreatedOrders = orders;
            }
        }

        private List<Order> CreateOrders(int count, Product product) => _fixture.Build<Order>()
            .Without(e => e.Id).With(e => e.Products, new List<Product> {product}).CreateMany(count).ToList();

        private Product CreateProduct() =>
            _fixture.Build<Product>().Without(e => e.Id).Without(e => e.Orders).Create();
    }
}
