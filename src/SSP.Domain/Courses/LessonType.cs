namespace SSP.Domain.Courses;

public record LessonType
{
    public static readonly LessonType Text = new("Text");
    public static readonly LessonType Video = new("Video");

    public string Name { get; }

    private LessonType(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}
