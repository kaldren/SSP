using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SSP.Domain.Courses;

namespace SSP.Infrastructure.Persistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(l => l.Id);

        builder.HasOne(l => l.Module)
            .WithMany(m => m.Lessons)
            .HasForeignKey(l => l.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(l => l.Title)
            .IsRequired()
            .HasMaxLength(200);

        var lessonTypeConverter = new ValueConverter<LessonType, string>(
            v => v.Name,
            v => v == "Video" ? LessonType.Video : LessonType.Text);

        builder.Property(l => l.Type)
            .IsRequired()
            .HasConversion(lessonTypeConverter);

        builder.Property(l => l.Content)
            .IsRequired(false);

        builder.Property(l => l.VideoUrl)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(l => l.Order)
            .IsRequired();

        builder.Property(l => l.CreatedAt)
            .IsRequired();

        builder.Property(l => l.UpdatedAt)
            .IsRequired(false);
    }
}
