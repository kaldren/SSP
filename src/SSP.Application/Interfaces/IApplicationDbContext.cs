using Microsoft.EntityFrameworkCore;
using SSP.Domain.Listings;

namespace SSP.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Listing> Listings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
