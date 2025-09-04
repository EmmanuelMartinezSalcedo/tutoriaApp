using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tutoriaBE.Core.SessionAggregate;
using tutoriaBE.Core.UserAggregate.Entities;
using tutoriaBE.Core.CourseAggregate;
using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.Infrastructure.Data.Config;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
  public void Configure(EntityTypeBuilder<Session> builder)
  {
    builder.HasOne<Tutor>()
      .WithMany()
      .HasForeignKey(s => s.TutorId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne<User>()
      .WithMany()
      .HasForeignKey(s => s.StudentId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne<Course>()
      .WithMany()
      .HasForeignKey(s => s.CourseId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(s => s.Status)
     .HasConversion(
       v => v.Name,
       v => SessionStatus.FromName(v, true)
     )
     .HasMaxLength(20)
     .IsRequired();

    builder.Property(s => s.CreatedAt)
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}
