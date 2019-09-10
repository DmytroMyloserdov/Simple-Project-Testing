using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class TestSteps
    {
        [Given(@"I start")]
        public void GivenIStart()
        {
        }

        [Then(@"succees")]
        public void ThenSuccees()
        {
            Assert.IsTrue(true);
        }

    }
}
