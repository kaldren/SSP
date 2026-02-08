using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSP.Application.Interfaces;
using SSP.Domain.Courses;

namespace SSP.Web.Pages;

public class LessonDetailModel : PageModel
{
    private readonly IApplicationDbContext _context;

    public LessonDetailModel(IApplicationDbContext context)
    {
        _context = context;
    }

    public Lesson Lesson { get; private set; } = default!;
    public Module Module { get; private set; } = default!;
    public Guid CourseId { get; private set; }
    public string CourseTitle { get; private set; } = default!;
    public string RenderedContent { get; private set; } = string.Empty;
    public string? YouTubeEmbedUrl { get; private set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var lesson = await _context.Courses
            .SelectMany(c => c.Modules)
            .SelectMany(m => m.Lessons)
            .Include(l => l.Module)
                .ThenInclude(m => m.Course)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lesson is null)
        {
            return NotFound();
        }

        Lesson = lesson;
        Module = lesson.Module;
        CourseId = lesson.Module.CourseId;
        CourseTitle = lesson.Module.Course.Title;

        if (lesson.Type == LessonType.Text && lesson.Content is not null)
        {
            RenderedContent = Markdig.Markdown.ToHtml(lesson.Content);
        }

        if (lesson.Type == LessonType.Video && lesson.VideoUrl is not null)
        {
            YouTubeEmbedUrl = ConvertToEmbedUrl(lesson.VideoUrl);
        }

        return Page();
    }

    private static string? ConvertToEmbedUrl(string url)
    {
        // Handle https://www.youtube.com/watch?v=VIDEO_ID
        if (url.Contains("youtube.com/watch", StringComparison.OrdinalIgnoreCase))
        {
            var uri = new Uri(url);
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            var videoId = query["v"];
            return videoId is not null ? $"https://www.youtube.com/embed/{videoId}" : null;
        }

        // Handle https://youtu.be/VIDEO_ID
        if (url.Contains("youtu.be", StringComparison.OrdinalIgnoreCase))
        {
            var uri = new Uri(url);
            var videoId = uri.AbsolutePath.TrimStart('/');
            return $"https://www.youtube.com/embed/{videoId}";
        }

        return null;
    }
}
