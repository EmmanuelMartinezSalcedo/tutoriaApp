using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.UseCases.Sessions.Book;
public record BookSessionCommand(
    int TutorId,
    int StudentId,
    int CourseId,
    ScheduleSlotDayOfWeek Day,
    List<int> ScheduleSlotIds
) : ICommand<Result<BookSessionDTO>>;
