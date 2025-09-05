using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.UseCases.Tutors.CreateScheduleSlot;
public record CreateScheduleSlotTutorCommand(int tutorId, ScheduleSlotDayOfWeek day, int startHour) : Ardalis.SharedKernel.ICommand<Result<ScheduleSlotDTO>>;
