using SSP.Domain.Courses;

namespace SSP.Domain.Content;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Course()
    {
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");

        if (title.Length > 200)
            throw new ArgumentException("Title cannot exceed 200 characters.");

        Title = title;
    }

    public void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");

        if (description.Length > 2000)
            throw new ArgumentException("Description cannot exceed 2000 characters.");

        Description = description;
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
            Difficulty = difficulty;
    }

}
