using tutoriaBE.Core.SessionAggregate;
using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.Infrastructure.Data.Config;
public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
  public void Configure(EntityTypeBuilder<Message> builder)
  {
    builder.Property(m => m.Content)
      .IsRequired();

    builder.Property(m => m.CreatedAt)
      .HasDefaultValueSql("CURRENT_TIMESTAMP");

    builder.HasOne<Chat>()
      .WithMany(c => c.Messages)
      .HasForeignKey(m => m.ChatId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne<User>()
      .WithMany(u => u.Messages)
      .HasForeignKey(m => m.SenderId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
