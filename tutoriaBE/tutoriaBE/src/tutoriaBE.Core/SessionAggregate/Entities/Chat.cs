namespace tutoriaBE.Core.SessionAggregate;

public class Chat : EntityBase
{
  public int SessionId { get; private set; }
  public bool IsActive { get; private set; } = true;

  // -----------------------------
  // Navigation properties
  // -----------------------------

  public virtual List<Message>? Messages { get; private set; }

  // -----------------------------
  // Constructors
  // -----------------------------
  protected Chat() { } // EF Core

  public Chat(int sessionId)
  {
    SessionId = Guard.Against.NegativeOrZero(sessionId, nameof(sessionId));
  }

  // -----------------------------
  // Update methods
  // -----------------------------

}
