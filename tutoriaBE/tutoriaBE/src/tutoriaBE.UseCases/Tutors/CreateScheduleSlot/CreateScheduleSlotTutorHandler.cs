using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Specifications;

namespace tutoriaBE.UseCases.Tutors.CreateScheduleSlot;

public class CreateScheduleSlotHandler : ICommandHandler<CreateScheduleSlotTutorCommand, Result<ScheduleSlotDTO>>
{
  private readonly IRepository<User> _userRepository;

  public CreateScheduleSlotHandler(IRepository<User> userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<Result<ScheduleSlotDTO>> Handle(CreateScheduleSlotTutorCommand request, CancellationToken cancellationToken)
  {
    var spec = new UserByIdSpec(request.tutorId);
    var user = await _userRepository.FirstOrDefaultAsync(spec, cancellationToken);

    if (user == null || user.TutorProfile == null)
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.tutorId),
        ErrorMessage = "User is not a tutor or does not exist."
      });

    var tutor = user.TutorProfile;

    var result = tutor.CreateScheduleSlot(request.day, request.startHour);
    if (!result.IsSuccess)
      return Result.Invalid(result.ValidationErrors);

    await _userRepository.UpdateAsync(user, cancellationToken);

    var slot = result.Value;

    var dto = new ScheduleSlotDTO(
      slot.DayOfWeek,
      slot.StartHour
    );

    return Result.Success(dto);
  }
}
