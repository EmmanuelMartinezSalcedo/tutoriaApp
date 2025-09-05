using Ardalis.SharedKernel;
using Ardalis.Result;
using tutoriaBE.Core.SessionAggregate;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Specifications;

namespace tutoriaBE.UseCases.Sessions.Book;

public class BookSessionHandler : ICommandHandler<BookSessionCommand, Result<BookSessionDTO>>
{
  private readonly IRepository<User> _userRepository;
  private readonly IRepository<Session> _sessionRepository;

  public BookSessionHandler(
    IRepository<User> userRepository,
    IRepository<Session> sessionRepository)
  {
    _userRepository = userRepository;
    _sessionRepository = sessionRepository;
  }

  public async Task<Result<BookSessionDTO>> Handle(BookSessionCommand request, CancellationToken cancellationToken)
  {
    // Validate student exists
    var studentSpec = new UserByIdSpec(request.StudentId);
    var student = await _userRepository.FirstOrDefaultAsync(studentSpec, cancellationToken);

    if (student == null)
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.StudentId),
        ErrorMessage = "Student does not exist."
      });

    // Validate tutor exists and load schedule slots
    var tutorSpec = new UserByIdSpec(request.TutorId);
    var tutorUser = await _userRepository.FirstOrDefaultAsync(tutorSpec, cancellationToken);

    if (tutorUser?.TutorProfile == null)
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.TutorId),
        ErrorMessage = "Tutor does not exist."
      });

    var scheduleSlots = tutorUser.TutorProfile.ScheduleSlots ?? new();

    // Validate schedule slots
    var selectedSlots = scheduleSlots
      .Where(slot => request.ScheduleSlotIds.Contains(slot.Id))
      .ToList();

    if (selectedSlots.Count != request.ScheduleSlotIds.Count)
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.ScheduleSlotIds),
        ErrorMessage = "One or more schedule slots do not exist for this tutor."
      });

    foreach (var slot in selectedSlots)
    {
      if (slot.Status != ScheduleSlotStatus.Available)
        return Result.Invalid(new ValidationError
        {
          Identifier = nameof(request.ScheduleSlotIds),
          ErrorMessage = $"Schedule slot {slot.Id} is not available."
        });

      if (slot.DayOfWeek != request.Day)
        return Result.Invalid(new ValidationError
        {
          Identifier = nameof(request.Day),
          ErrorMessage = $"Schedule slot {slot.Id} is not on the requested day."
        });
    }

    // Create session
    var result = Session.BookSession(
      request.TutorId,
      request.StudentId,
      request.CourseId,
      request.Day,
      request.ScheduleSlotIds
    );

    if (!result.IsSuccess)
      return Result.Invalid(result.ValidationErrors);

    var session = result.Value;

    // Save session
    await _sessionRepository.AddAsync(session, cancellationToken);
    await _sessionRepository.SaveChangesAsync(cancellationToken);

    // Mark slots as booked (in-memory, will be persisted via Tutor aggregate)
    foreach (var slot in selectedSlots)
    {
      slot.book();
    }
    await _userRepository.UpdateAsync(tutorUser, cancellationToken);

    // Determine start and end hours
    var startHour = selectedSlots.Min(s => s.StartHour);
    var endHour = selectedSlots.Max(s => s.StartHour) + 1;

    var dto = new BookSessionDTO(
      request.Day.Name,
      startHour,
      endHour
    );

    return Result.Success(dto);
  }
}
