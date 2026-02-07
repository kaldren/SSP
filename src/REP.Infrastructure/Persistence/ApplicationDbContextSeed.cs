using REP.Domain.Listings;

namespace REP.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Listings.Any())
            return;

        var now = DateTimeOffset.UtcNow;

        context.Listings.AddRange(
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "Modern 2-Bedroom Apartment",
                Description = "Spacious apartment in the city center with a balcony and underground parking.",
                Price = new Price(185_000m, "EUR"),
                CreatedAt = now,
                UpdatedAt = now
            },
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "Cozy Studio Near the Park",
                Description = "Bright studio with open floor plan, perfect for young professionals.",
                Price = new Price(72_500m, "EUR"),
                CreatedAt = now,
                UpdatedAt = now
            },
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "Family House with Garden",
                Description = "Detached 4-bedroom house with a large garden and double garage.",
                Price = new Price(340_000m, "EUR"),
                CreatedAt = now,
                UpdatedAt = now
            },
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "Penthouse with Panoramic Views",
                Description = "Luxury penthouse on the top floor featuring floor-to-ceiling windows.",
                Price = new Price(520_000m, "EUR"),
                CreatedAt = now,
                UpdatedAt = now
            },
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "Renovated 1-Bedroom Flat",
                Description = "Recently renovated flat close to public transport and shopping areas.",
                Price = new Price(98_000m, "EUR"),
                CreatedAt = now,
                UpdatedAt = now
            }
        );

        await context.SaveChangesAsync();
    }
}
