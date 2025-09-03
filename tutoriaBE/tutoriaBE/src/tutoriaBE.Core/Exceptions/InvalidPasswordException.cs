namespace tutoriaBE.Core.Exceptions;

public class InvalidPasswordException : Exception
{
  public InvalidPasswordException(string message) : base(message) { }
}

public class IncorrectPasswordException : Exception
{
  public IncorrectPasswordException() : base("Incorrect password") { }
}
