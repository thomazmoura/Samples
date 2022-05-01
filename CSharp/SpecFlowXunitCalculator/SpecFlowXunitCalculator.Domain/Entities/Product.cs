namespace SpecFlowXunitCalculator.Domain.Entities
{
    public record Product
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
    }
}
