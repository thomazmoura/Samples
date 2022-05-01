using System.Linq;
using SpecFlowXunitCalculator.Domain.Entities;

namespace SpecFlowXunitCalculator.Domain.Services
{
    public interface IPricingService
    {
        decimal GetBasketTotalAmount(Basket basket);
    }

    public class PricingService: IPricingService
    {
        public decimal GetBasketTotalAmount(Basket basket)
        {
            if(!basket.Products.Any())
                return 0;

            var basketValue = basket.Products
                .Sum(item => item.Price);

            if(basket.User.IsLoggedIn)
                return basketValue * 0.95m;
            else
                return basketValue;
        }
    }
}
