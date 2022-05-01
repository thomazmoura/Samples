using System.Collections.Generic;
using FluentAssertions;
using SpecFlowXunitCalculator.Domain.Entities;
using SpecFlowXunitCalculator.Domain.Services;
using TechTalk.SpecFlow;

namespace SpecFlowXunitCalculator.Tests.Steps
{
    [Binding]
    public class LoggedInDiscountSteps
    {
        private User _user;
        private Basket _basket;
        private PricingService _pricingService;

        public LoggedInDiscountSteps()
        {
            _pricingService = new PricingService();
        }

        [Given(@"a user that is not logged in")]
        public void GivenAUserThatIsNotLoggedIn()
        {
            _user = new User
            {
                IsLoggedIn = false
            };
        }

        [Given(@"a user that is logged in")]
        public void GivenAUserThatIsLoggedIn()
        {
            _user = new User
            {
                IsLoggedIn = true
            };
        }

        [Given(@"an empty basket")]
        public void GivenAnEmptyBasket()
        {
            _basket = new Basket
            {
                User = _user,
                Products = new List<Product>()
            };
        }

        [When(@"a (.*) that costs (.*) GBP is added to the basket")]
        public void WhenAProductThatCostsPriceIsAddedToTheBasket(
            string productName,
            decimal price
        )
        {
            _basket.Products.Add(new Product
            {
                Name = productName,
                Price = price
            });
        }

        [Then(@"the basket value is (.*) GBP")]
        public void ThenTheBasketValueIsPriceGBP(decimal expectedPrice)
        {
            var totalAmount = _pricingService.GetBasketTotalAmount(_basket);
            totalAmount.Should().Be(expectedPrice);
        }
   }
}
