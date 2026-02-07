namespace SSP.Domain.Listings;

public class Listing
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Price? Price { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
