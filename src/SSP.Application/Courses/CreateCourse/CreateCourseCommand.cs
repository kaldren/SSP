using LiteBus.Commands.Abstractions;
using SSP.Domain.Content;
using SSP.Domain.Courses;

namespace SSP.Application.Courses.CreateCourse;

public record CreateCourseCommand(CreateCourseRequest request) : ICommand<CreateCourseResponse>;

public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, CreateCourseResponse>
{
    private readonly ICourseRepository _courseRepository;

    public CreateCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CreateCourseResponse> HandleAsync(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var course = new Course();
        course.SetTitle(command.request.Name);
        course.SetDescription(command.request.Description);
        course.SetDifficulty(command.request.Difficulty);

        await _courseRepository.AddAsync(course, cancellationToken);
        await _courseRepository.SaveChangesAsync(cancellationToken);

        return new CreateCourseResponse(course.Id);
    }
}
