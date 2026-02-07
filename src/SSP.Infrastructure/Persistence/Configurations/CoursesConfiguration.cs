using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SSP.Domain.Content;
using SSP.Domain.Courses;
using SSP.Domain.Listings;

namespace SSP.Infrastructure.Persistence.Configurations;

public class CoursesConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .HasMaxLength(2000);

        var difficultyConverter = new ValueConverter<DifficultyLevel, string>(
            v => v.Name,
            v => v == "Beginner" ? DifficultyLevel.Beginner
               : v == "Intermediate" ? DifficultyLevel.Intermediate
               : DifficultyLevel.Advanced);

        builder.Property(c => c.Difficulty)
            .IsRequired()
            .HasConversion(difficultyConverter);
    }
}
