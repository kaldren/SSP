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
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Installing .NET SDK",
                            Type = LessonType.Text,
                            Order = 1,
                            CreatedAt = now,
                            Content = "## Installing the .NET SDK\n\nTo get started with C#, you need the .NET SDK.\n\n### Steps\n\n1. Visit the [official .NET download page](https://dotnet.microsoft.com/download)\n2. Download the latest **LTS** version\n3. Run the installer for your operating system\n4. Verify the installation:\n\n```bash\ndotnet --version\n```\n\n### What's included?\n\n- The **runtime** for executing .NET applications\n- The **CLI tools** for building, running, and publishing projects\n- The **base class libraries** used by all .NET applications"
                        },
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Your First C# Program",
                            Type = LessonType.Video,
                            Order = 2,
                            CreatedAt = now,
                            VideoUrl = "https://www.youtube.com/watch?v=ravLFzIGuCM"
                        },
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Hello World",
                            Type = LessonType.Text,
                            Order = 3,
                            CreatedAt = now,
                            Content = "## Your First C# Program\n\nLet's create and run a simple console application.\n\n### Create the project\n\n```bash\ndotnet new console -n HelloWorld\ncd HelloWorld\n```\n\n### The generated code\n\nOpen `Program.cs` — you'll see:\n\n```csharp\nConsole.WriteLine(\"Hello, World!\");\n```\n\nThis single line is all you need — .NET uses **top-level statements**.\n\n### Run it\n\n```bash\ndotnet run\n```\n\n> **Output:** `Hello, World!`\n\nCongratulations — you've written your first C# program!"
                        },
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
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Primitive Types",
                            Type = LessonType.Text,
                            Order = 1,
                            CreatedAt = now,
                            Content = "## Primitive Types in C#\n\nC# is a **strongly typed** language. Every variable has a type.\n\n### Common types\n\n| Type | Description | Example |\n|------|-------------|--------|\n| `int` | 32-bit integer | `int age = 30;` |\n| `double` | 64-bit float | `double pi = 3.14;` |\n| `bool` | Boolean | `bool ok = true;` |\n| `char` | Single character | `char c = 'A';` |\n| `string` | Text | `string s = \"Hi\";` |\n\n### Type inference with `var`\n\n```csharp\nvar name = \"Alice\"; // inferred as string\nvar count = 42;     // inferred as int\n```\n\nUse `var` when the type is obvious from the right-hand side."
                        },
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "C# Data Types Explained",
                            Type = LessonType.Video,
                            Order = 2,
                            CreatedAt = now,
                            VideoUrl = "https://www.youtube.com/watch?v=_D05p-9jNPo"
                        },
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
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Classes and Objects",
                            Type = LessonType.Text,
                            Order = 1,
                            CreatedAt = now,
                            Content = "## Classes and Objects\n\nA **class** is a blueprint for creating objects.\n\n### Defining a class\n\n```csharp\npublic class Person\n{\n    public string Name { get; set; }\n    public int Age { get; set; }\n\n    public string Greet() => $\"Hi, I'm {Name}!\";\n}\n```\n\n### Creating objects\n\n```csharp\nvar alice = new Person { Name = \"Alice\", Age = 30 };\nConsole.WriteLine(alice.Greet());\n```\n\n### Key concepts\n\n- **Properties** expose data with getters and setters\n- **Methods** define behavior\n- **Constructors** initialize state when an object is created"
                        },
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "OOP Fundamentals in C#",
                            Type = LessonType.Video,
                            Order = 2,
                            CreatedAt = now,
                            VideoUrl = "https://www.youtube.com/watch?v=pTB0EiLXUC8"
                        },
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Interfaces",
                            Type = LessonType.Text,
                            Order = 3,
                            CreatedAt = now,
                            Content = "## Interfaces\n\nAn **interface** defines a contract that classes must implement.\n\n### Defining an interface\n\n```csharp\npublic interface IShape\n{\n    double Area();\n    double Perimeter();\n}\n```\n\n### Implementing an interface\n\n```csharp\npublic class Circle : IShape\n{\n    public double Radius { get; set; }\n\n    public double Area() => Math.PI * Radius * Radius;\n    public double Perimeter() => 2 * Math.PI * Radius;\n}\n```\n\n### Why use interfaces?\n\n- **Decoupling** — depend on abstractions, not concrete types\n- **Testability** — mock interfaces in unit tests\n- **Polymorphism** — treat different implementations uniformly"
                        },
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
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Lists and Dictionaries",
                            Type = LessonType.Text,
                            Order = 1,
                            CreatedAt = now,
                            Content = "## Collections in C#\n\nThe `System.Collections.Generic` namespace provides type-safe collections.\n\n### List\n\n```csharp\nvar numbers = new List<int> { 1, 2, 3 };\nnumbers.Add(4);\n```\n\n### Dictionary\n\n```csharp\nvar ages = new Dictionary<string, int>\n{\n    [\"Alice\"] = 30,\n    [\"Bob\"] = 25\n};\nConsole.WriteLine(ages[\"Alice\"]); // 30\n```\n\n### Choosing the right collection\n\n- **List** — ordered, index-based access\n- **Dictionary** — key-based lookup, O(1) average\n- **HashSet** — unique elements, fast membership tests"
                        },
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "LINQ in C#",
                            Type = LessonType.Video,
                            Order = 2,
                            CreatedAt = now,
                            VideoUrl = "https://www.youtube.com/watch?v=yClSNGol2Vg"
                        },
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
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Layered Architecture",
                            Type = LessonType.Text,
                            Order = 1,
                            CreatedAt = now,
                            Content = "## Layered Architecture\n\nA **layered architecture** separates concerns into horizontal layers.\n\n### Typical layers\n\n1. **Presentation** — UI, controllers, view models\n2. **Application** — use cases, orchestration, DTOs\n3. **Domain** — entities, value objects, business rules\n4. **Infrastructure** — databases, external APIs, file system\n\n### Dependency rule\n\nDependencies point **inward** — inner layers never reference outer layers.\n\n```\nPresentation -> Application -> Domain <- Infrastructure\n```\n\n### Benefits\n\n- Clear separation of concerns\n- Each layer can be tested independently\n- Easy to swap infrastructure components"
                        },
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Clean Architecture Explained",
                            Type = LessonType.Video,
                            Order = 2,
                            CreatedAt = now,
                            VideoUrl = "https://www.youtube.com/watch?v=dK4Yb6-LKAg"
                        },
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
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "Caching Strategies",
                            Type = LessonType.Text,
                            Order = 1,
                            CreatedAt = now,
                            Content = "## Caching Strategies\n\nCaching reduces latency by storing frequently accessed data closer to the consumer.\n\n### Cache-Aside (Lazy Loading)\n\n1. Check cache for data\n2. If miss — load from database\n3. Store result in cache\n4. Return data\n\n### Write-Through\n\nWrites go to **both** the cache and the database simultaneously.\n\n### Distributed caching\n\nFor multi-instance deployments, use a shared cache like **Redis**:\n\n```csharp\nbuilder.Services.AddStackExchangeRedisCache(options =>\n{\n    options.Configuration = \"localhost:6379\";\n});\n```\n\n### Eviction policies\n\n- **TTL** — expire after a fixed duration\n- **LRU** — evict least recently used entries\n- **Sliding expiration** — reset timer on each access"
                        },
                        new Lesson
                        {
                            Id = Guid.NewGuid(),
                            Title = "System Design Fundamentals",
                            Type = LessonType.Video,
                            Order = 2,
                            CreatedAt = now,
                            VideoUrl = "https://www.youtube.com/watch?v=Y-Gl4HEyeUQ"
                        },
                    ]
                }
            ]
        };

        context.Courses.AddRange(introCourse, intermediateCourse, advancedCourse);
        await context.SaveChangesAsync();
    }
}
