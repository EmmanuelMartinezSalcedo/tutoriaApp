namespace tutoriaBE.Core.SessionAggregate;

public class SessionSlot : EntityBase
{
  public int SessionId { get; private set; }
  public int ScheduleSlotId { get; private set; }

  // -----------------------------
  // Navigation properties
  // -----------------------------

  // -----------------------------
  // Constructors
  // -----------------------------
  private SessionSlot() { } // EF Core

  public SessionSlot(int sessionId, int scheduleSlotId)
  {
    SessionId = Guard.Against.NegativeOrZero(sessionId, nameof(sessionId));
    ScheduleSlotId = Guard.Against.NegativeOrZero(scheduleSlotId, nameof(scheduleSlotId));
  }

  // -----------------------------
  // Update methods
  // -----------------------------

}
