namespace RabbitMQFlows.Domain.Entities;

public class Purchase
{
    public int Id { get; set; }
    public DateTimeOffset PurchaseDate { get; set; }
    public List<PurchaseItem> Items { get; set; }

    public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
}

public class PurchaseItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;

    public Product Product { get; set; }
    public Purchase Purchase { get; set; }

    public int PurchaseId { get; set; }
    public int ProductId { get; set; }
}
