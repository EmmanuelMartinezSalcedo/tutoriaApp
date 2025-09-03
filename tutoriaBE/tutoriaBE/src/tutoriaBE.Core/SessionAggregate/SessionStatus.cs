namespace tutoriaBE.Core.SessionAggregate;

public class SessionStatus : SmartEnum<SessionStatus>
{
  public static readonly SessionStatus Pending = new(nameof(Pending), 1);
  public static readonly SessionStatus Complete = new(nameof(Complete), 2);
  public static readonly SessionStatus Cancelled = new(nameof(Cancelled), 3);

  protected SessionStatus(string name, int value) : base(name, value) { }
}

