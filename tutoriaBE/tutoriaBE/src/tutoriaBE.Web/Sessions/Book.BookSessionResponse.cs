namespace tutoriaBE.Web.Sessions;

public class BookSessionResponse(string day, int startHour, int endHour)
{
  public string Day { get; set; } = day;
  public int StartHour { get; set; } = startHour; 
  public int EndHour { get; set; } = endHour;

}
