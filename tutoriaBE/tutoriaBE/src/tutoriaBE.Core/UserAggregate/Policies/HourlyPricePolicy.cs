namespace tutoriaBE.Core.UserAggregate.Policies;
public class HourlyPricePolicy
{
  public const decimal MinimumPrice = 10.0m;
  public const decimal MaximumPrice = 100.0m;

  public static bool HasMinimumPrice(decimal? hourlyPrice)
  {
    return hourlyPrice.HasValue && hourlyPrice >= MinimumPrice;
  }
  public static bool HasMaximumPrice(decimal? hourlyPrice)
  {
    return hourlyPrice.HasValue && hourlyPrice <= MaximumPrice;
  }

  public static List<string> Validate(decimal? hourlyPrice)
  {
    var errors = new List<string>();

    if (!HasMinimumPrice(hourlyPrice))
      errors.Add($"Hourly price must be at least {MinimumPrice}.");

    if (!HasMaximumPrice(hourlyPrice))
      errors.Add($"Hourly price must be at most {MaximumPrice}.");

    return errors;
  }
}
