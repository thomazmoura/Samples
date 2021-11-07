using TechTalk.SpecFlow;

namespace SpecFlowXunitCalculator.Tests.Steps
{
    [Binding]
    public class LoggedInDiscountSteps
    {
        [Given(@"a user that is not logged in")]
        public void GivenAUserThatIsNotLoggedIn()
        {
            ScenarioContext.StepIsPending();
        }

        [Given(@"a user that is logged in")]
        public void GivenAUserThatIsLoggedIn()
        {
            ScenarioContext.StepIsPending();
        }

        [Given(@"an empty basket")]
        public void GivenAnEmptyBasket()
        {
            ScenarioContext.StepIsPending();
        }

        [When(@"a (.*) that costs (.*) GBP is added to the basket")]
        public void WhenAProductThatCostsPriceIsAddedToTheBasket(
            string productName,
            decimal price
        )
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the basket value is (.*) GBP")]
        public void ThenTheBasketValueIsPriceGBP()
        {
            ScenarioContext.StepIsPending();
        }
   }
}
