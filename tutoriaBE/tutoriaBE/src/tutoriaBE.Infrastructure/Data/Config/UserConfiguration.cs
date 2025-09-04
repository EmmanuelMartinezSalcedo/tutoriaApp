using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Entities;

namespace tutoriaBE.Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    
    builder.Property(u => u.FirstName)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(u => u.LastName)
      .IsRequired()
      .HasMaxLength(100);

    builder.OwnsOne(u => u.Email, email =>
    {
      email.Property(e => e.Value)
        .HasColumnName("Email")
        .IsRequired()
        .HasMaxLength(255);
      email.HasIndex(e => e.Value).IsUnique();
    });

    builder.Property(u => u.PasswordHash)
      .IsRequired();

    builder.Property(u => u.ProfilePhotoPath)
      .HasMaxLength(500);

    builder.Property(s => s.Role)
     .HasConversion(
       v => v.Name,
       v => UserRole.FromName(v, true)
     )
     .HasMaxLength(20)
     .IsRequired();

    builder.Property(u => u.CreatedAt)
      .HasDefaultValueSql("CURRENT_TIMESTAMP");

    builder.HasOne(u => u.TutorProfile)
      .WithOne()
      .HasForeignKey<Tutor>(t => t.Id)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
