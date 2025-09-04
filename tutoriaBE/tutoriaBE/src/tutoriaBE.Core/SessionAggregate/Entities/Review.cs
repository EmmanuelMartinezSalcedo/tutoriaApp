namespace tutoriaBE.Core.SessionAggregate;

public class Review : EntityBase
{
  public int SessionId { get; private set; }
  public decimal Rating { get; private set; } = default!;
  public string Comment { get; private set; } = default!;
  public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

  // -----------------------------
  // Navigation properties
  // -----------------------------

  // -----------------------------
  // Constructors
  // -----------------------------
  private Review() { } // EF Core

  public Review(int sessionId, decimal rating, string comment)
  {
    SessionId = Guard.Against.NegativeOrZero(sessionId, nameof(sessionId));
    Rating = Guard.Against.OutOfRange(rating, nameof(rating), 1, 5);
    Comment = Guard.Against.NullOrEmpty(comment, nameof(comment));
  }

  // -----------------------------
  // Update methods
  // -----------------------------

}
