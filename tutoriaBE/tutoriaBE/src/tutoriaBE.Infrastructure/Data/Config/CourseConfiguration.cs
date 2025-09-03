using tutoriaBE.Core.CourseAggregate;

namespace tutoriaBE.Infrastructure.Data.Config;
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
  public void Configure(EntityTypeBuilder<Course> builder)
  {
    builder.Property(c => c.Title)
             .IsRequired()
             .HasMaxLength(200);

    builder.Property(c => c.Description)
           .HasMaxLength(1000);

    builder.HasMany(c => c.TutorCourses)
           .WithOne(tc => tc.Course)
           .HasForeignKey(tc => tc.CourseId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
