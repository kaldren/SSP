using SSP.Domain.Content;
using SSP.Domain.Courses;

namespace SSP.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Courses.Any())
            return;

        var now = DateTimeOffset.UtcNow;

        var introCourse = new Course
        {
            Id = Guid.NewGuid(),
            Title = "Introduction to Programming",
            Description = "Learn the basics of programming using C#.",
            Difficulty = DifficultyLevel.Beginner,
            CreatedAt = now,
            Modules =
            [
                new Module
                {
                    Id = Guid.NewGuid(),
                    Title = "Getting Started",
                    Description = "Set up your development environment and write your first program.",
                    Order = 1,
                    CreatedAt = now,
                    Lessons =
                    [
                        new Lesson { Id = Guid.NewGuid(), Title = "Installing .NET SDK", Content = "Download and install the .NET SDK from the official website.", Order = 1, CreatedAt = now },
                        new Lesson { Id = Guid.NewGuid(), Title = "Hello World", Content = "Create your first console application and run it.", Order = 2, CreatedAt = now },
                    ]
                },
                new Module
                {
                    Id = Guid.NewGuid(),
                    Title = "Variables and Types",
                    Description = "Understand data types, variables, and basic operations.",
                    Order = 2,
                    CreatedAt = now,
                    Lessons =
                    [
                        new Lesson { Id = Guid.NewGuid(), Title = "Primitive Types", Content = "Learn about int, double, bool, char, and string.", Order = 1, CreatedAt = now },
                        new Lesson { Id = Guid.NewGuid(), Title = "Operators and Expressions", Content = "Arithmetic, comparison, and logical operators.", Order = 2, CreatedAt = now },
                    ]
                }
            ]
        };

        var intermediateCourse = new Course
        {
            Id = Guid.NewGuid(),
            Title = "C# Concepts",
            Description = "Deep dive into intermediate C# programming concepts.",
            Difficulty = DifficultyLevel.Intermediate,
            CreatedAt = now,
            Modules =
            [
                new Module
                {
                    Id = Guid.NewGuid(),
                    Title = "Object-Oriented Programming",
                    Description = "Classes, inheritance, polymorphism, and encapsulation.",
                    Order = 1,
                    CreatedAt = now,
                    Lessons =
                    [
                        new Lesson { Id = Guid.NewGuid(), Title = "Classes and Objects", Content = "Define classes, create objects, and use constructors.", Order = 1, CreatedAt = now },
                        new Lesson { Id = Guid.NewGuid(), Title = "Inheritance", Content = "Extend classes and override behavior using inheritance.", Order = 2, CreatedAt = now },
                        new Lesson { Id = Guid.NewGuid(), Title = "Interfaces", Content = "Define contracts with interfaces and implement them.", Order = 3, CreatedAt = now },
                    ]
                },
                new Module
                {
                    Id = Guid.NewGuid(),
                    Title = "Collections and LINQ",
                    Description = "Work with collections and query data using LINQ.",
                    Order = 2,
                    CreatedAt = now,
                    Lessons =
                    [
                        new Lesson { Id = Guid.NewGuid(), Title = "Lists and Dictionaries", Content = "Use generic collections to store and retrieve data.", Order = 1, CreatedAt = now },
                        new Lesson { Id = Guid.NewGuid(), Title = "LINQ Queries", Content = "Filter, project, and aggregate data with LINQ.", Order = 2, CreatedAt = now },
                    ]
                }
            ]
        };

        var advancedCourse = new Course
        {
            Id = Guid.NewGuid(),
            Title = "Advanced System Design",
            Description = "Learn how to design scalable systems.",
            Difficulty = DifficultyLevel.Advanced,
            CreatedAt = now,
            Modules =
            [
                new Module
                {
                    Id = Guid.NewGuid(),
                    Title = "Architecture Patterns",
                    Description = "Explore common software architecture patterns.",
                    Order = 1,
                    CreatedAt = now,
                    Lessons =
                    [
                        new Lesson { Id = Guid.NewGuid(), Title = "Layered Architecture", Content = "Organize code into presentation, business, and data layers.", Order = 1, CreatedAt = now },
                        new Lesson { Id = Guid.NewGuid(), Title = "Microservices", Content = "Decompose applications into independently deployable services.", Order = 2, CreatedAt = now },
                    ]
                },
                new Module
                {
                    Id = Guid.NewGuid(),
                    Title = "Scalability",
                    Description = "Techniques for scaling applications under load.",
                    Order = 2,
                    CreatedAt = now,
                    Lessons =
                    [
                        new Lesson { Id = Guid.NewGuid(), Title = "Caching Strategies", Content = "Use in-memory and distributed caches to reduce latency.", Order = 1, CreatedAt = now },
                        new Lesson { Id = Guid.NewGuid(), Title = "Load Balancing", Content = "Distribute traffic across multiple service instances.", Order = 2, CreatedAt = now },
                    ]
                }
            ]
        };

        context.Courses.AddRange(introCourse, intermediateCourse, advancedCourse);
        await context.SaveChangesAsync();
    }
}
