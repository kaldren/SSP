using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSP.Application.Interfaces;
using SSP.Domain.Content;

namespace SSP.Web.Pages;

public class CourseDetailModel : PageModel
{
    private readonly IApplicationDbContext _context;

    public CourseDetailModel(IApplicationDbContext context)
    {
        _context = context;
    }

    public Course Course { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var course = await _context.Courses
            .Include(c => c.Modules.OrderBy(m => m.Order))
                .ThenInclude(m => m.Lessons.OrderBy(l => l.Order))
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course is null)
        {
            return NotFound();
        }

        Course = course;
        return Page();
    }
}
