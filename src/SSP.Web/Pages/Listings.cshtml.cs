using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSP.Application.Interfaces;
using SSP.Domain.Listings;

namespace SSP.Web.Pages;

public class ListingsModel : PageModel
{
    private readonly IApplicationDbContext _context;

    public ListingsModel(IApplicationDbContext context)
    {
        _context = context;
    }

    public IReadOnlyList<Listing> Listings { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Listings = await _context.Listings
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync();
    }
}
