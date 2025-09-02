namespace tutoriaBE.Core.UserAggregate;

public class User : EntityBase, IAggregateRoot
{
  public string Name { get; private set; } = default!;
  public string Email { get; private set; } = default!;
  public string PasswordHash { get; private set; } = default!;
  public DateTime CreatedAt { get; private set; }

  public User(string name, string email, string passwordHash)
  {
    UpdateName(name);
    UpdateEmail(email);
    PasswordHash = Guard.Against.NullOrEmpty(passwordHash, nameof(passwordHash));
    CreatedAt = DateTime.UtcNow;
  }

  public User UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
    return this;
  }

  public User UpdateEmail(string newEmail)
  {
    Email = Guard.Against.NullOrEmpty(newEmail, nameof(newEmail));
    return this;
  }
}
