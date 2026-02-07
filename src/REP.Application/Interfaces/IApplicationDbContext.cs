using Microsoft.EntityFrameworkCore;
using REP.Domain.Listings;

namespace REP.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Listing> Listings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
