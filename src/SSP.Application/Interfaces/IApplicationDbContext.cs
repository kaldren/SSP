using Microsoft.EntityFrameworkCore;
using SSP.Domain.Content;

namespace SSP.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Course> Courses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
