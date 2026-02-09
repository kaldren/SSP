using SSP.Domain.Content;
using SSP.Domain.Courses;

namespace SSP.Infrastructure.Persistence.Repositories;

public class CourseRepository(ApplicationDbContext context) : Repository<Course>(context), ICourseRepository
{
}
