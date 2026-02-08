using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSP.Application.Interfaces;
using SSP.Domain.Content;

namespace SSP.Web.Pages;

public class CoursesModel : PageModel
{
    private readonly IApplicationDbContext _context;

    public CoursesModel(IApplicationDbContext context)
    {
        _context = context;
    }

    public IReadOnlyList<Course> Courses { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Courses = await _context.Courses
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}
