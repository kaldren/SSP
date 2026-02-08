using SSP.Domain.Courses;

namespace SSP.Application.Courses.CreateCourse;

public record CreateCourseRequest(
    string Name,
    string Description,
    DifficultyLevel Difficulty
);