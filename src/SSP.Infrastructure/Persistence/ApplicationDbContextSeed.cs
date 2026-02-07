using SSP.Domain.Content;
using SSP.Domain.Courses;
using SSP.Domain.Listings;

namespace SSP.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Courses.Any())
            return;

        var now = DateTimeOffset.UtcNow;

        context.Courses.AddRange(
             new Course
             {
                 Id = Guid.NewGuid(),
                 Title = "Introduction to Programming",
                 Description = "Learn the basics of programming using C#.",
                 Difficulty = DifficultyLevel.Beginner,
                 CreatedAt = now
             },
             new Course
             {
                 Id = Guid.NewGuid(),
                 Title = "C# Concepts",
                 Description = "Deep dive into intermediate C# programming concepts.",
                 Difficulty = DifficultyLevel.Intermediate,
                 CreatedAt = now
             },
             new Course
             {
                 Id = Guid.NewGuid(),
                 Title = "Advanced System Design",
                 Description = "Learn how to design scalable systems.",
                 Difficulty = DifficultyLevel.Advanced,
                 CreatedAt = now
             }
         );

        await context.SaveChangesAsync();
    }
}
