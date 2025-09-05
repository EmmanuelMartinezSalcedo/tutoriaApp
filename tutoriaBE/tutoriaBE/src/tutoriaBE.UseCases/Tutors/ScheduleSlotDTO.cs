using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.UseCases.Tutors;
public record ScheduleSlotDTO(ScheduleSlotDayOfWeek DayOfWeek, int StartHour);
