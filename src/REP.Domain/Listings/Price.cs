namespace REP.Domain.Listings;

public record Price
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    public Price(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty.", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpper();
    }
    public override string ToString() => $"{Amount} {Currency}";
}