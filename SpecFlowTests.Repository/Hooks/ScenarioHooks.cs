using AutoFixture;
using BoDi;
using SqlServer.Context;
using SqlServer.Interfaces;
using SqlServer.Repositories;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Repository.Hooks
{
    [Binding]
    public class ScenarioHooks
    {
        private readonly IObjectContainer _objectContainer;

        public ScenarioHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeRepositories()
        {
            var dbContext =
                new SimpleContext(
                    "data source=.\\SQLEXPRESS;initial catalog=SimpleTesting;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            _objectContainer.RegisterInstanceAs(dbContext, typeof(SimpleContext));
            _objectContainer.RegisterTypeAs<ProductRepository, IProductRepository>();
            _objectContainer.RegisterTypeAs<OrderRepository, IOrderRepository>();
            _objectContainer.RegisterInstanceAs(new Fixture(), typeof(Fixture));
        }
    }
}
