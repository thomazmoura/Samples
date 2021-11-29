var currency = "Bolsos";
var firstMoney = new Money(currency, 10);
var secondMoney = new Money(currency, 10);
var thirdMoney = firstMoney.Add(secondMoney);
Console.WriteLine($"Adding {firstMoney.Amount} {firstMoney.Currency} to {secondMoney.Amount} {secondMoney.Currency} results in {thirdMoney.Amount} {thirdMoney.Currency}");

public record Money(string Currency, int Amount)
{
    public Money Add(Money other) {
        if(Currency == other.Currency)
            return new Money(Currency, Amount + other.Amount);
        throw new ArgumentException($"Cannot add {other} to this");
    }
}

