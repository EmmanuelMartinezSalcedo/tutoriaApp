using Ardalis.Specification;

namespace tutoriaBE.Core.SessionAggregate.Specifications;

public class SlotsByIds : Specification<ScheduleSlot>
{
  public SlotsByIds(List<int> ids)
  {
    Query
        .Where(slot => ids.Contains(slot.Id));
  }
}
