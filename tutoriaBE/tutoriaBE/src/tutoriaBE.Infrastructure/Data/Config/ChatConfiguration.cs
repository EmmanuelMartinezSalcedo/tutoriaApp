using tutoriaBE.Core.SessionAggregate;

namespace tutoriaBE.Infrastructure.Data.Config;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
  public void Configure(EntityTypeBuilder<Chat> builder)
  {
    builder.HasOne<Session>()
      .WithOne(s => s.Chat)
      .HasForeignKey<Chat>(c => c.SessionId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(c => c.IsActive)
      .IsRequired()
      .HasDefaultValue(true);
  }
}
