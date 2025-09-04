using tutoriaBE.Core.CourseAggregate;
using tutoriaBE.Core.UserAggregate.Entities;

namespace tutoriaBE.Infrastructure.Data.Config;

public class TutorCourseConfiguration : IEntityTypeConfiguration<TutorCourse>
{
  public void Configure(EntityTypeBuilder<TutorCourse> builder)
  {
    builder.HasKey(tc => new { tc.TutorId, tc.CourseId });

    builder.Ignore(ss => ss.Id);

    builder.Property(tc => tc.HourlyPrice)
      .IsRequired()
      .HasColumnType("decimal(10,2)");

    builder.HasOne<Tutor>()
      .WithMany(t => t.TutorCourses)
      .HasForeignKey(tc => tc.TutorId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne<Course>()
      .WithMany(c => c.TutorCourses)
      .HasForeignKey(tc => tc.CourseId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
