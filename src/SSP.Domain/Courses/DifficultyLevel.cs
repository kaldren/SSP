namespace SSP.Domain.Courses;

public record DifficultyLevel
{
    public static readonly DifficultyLevel Beginner = new DifficultyLevel("Beginner");
    public static readonly DifficultyLevel Intermediate = new DifficultyLevel("Intermediate");
    public static readonly DifficultyLevel Advanced = new DifficultyLevel("Advanced");
    public string Name { get; }
    private DifficultyLevel(string name)
    {
        Name = name;
    }
    public override string ToString() => Name;
}