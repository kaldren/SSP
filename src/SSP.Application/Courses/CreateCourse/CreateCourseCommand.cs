using LiteBus.Commands.Abstractions;

namespace SSP.Application.Courses.CreateCourse;

public record CreateCourseCommand(CreateCourseRequest request) : ICommand<CreateCourseResponse>;

public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, CreateCourseResponse>
{
    private readonly ICourseRepository _courseRepository;
    public CreateCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }
    public async Task<CreateCourseResponse> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
    }
}
