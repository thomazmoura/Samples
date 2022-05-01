using System.Collections.Generic;

namespace SpecFlowXunitCalculator.Domain.Entities
{
    public class Basket
    {
        public User User { get; set; }
        public List<Product> Products { get; set; }
    }
}
