namespace tutoriaBE.Core.SessionAggregate;

public class Message : EntityBase
{
  public int ChatId { get; private set; }
  public int SenderId { get; private set; } = default!;
  public string Content { get; private set; } = default!;
  public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

  // -----------------------------
  // Navigation properties
  // -----------------------------

  // -----------------------------
  // Constructors
  // -----------------------------
  private Message() { } // EF Core

  public Message(int chatId, int senderId, string content)
  {
    ChatId = Guard.Against.NegativeOrZero(chatId, nameof(chatId));
    SenderId = Guard.Against.NegativeOrZero(senderId, nameof(senderId));
    Content = Guard.Against.NullOrEmpty(content, nameof(content));
  }

  // -----------------------------
  // Update methods
  // -----------------------------

}
