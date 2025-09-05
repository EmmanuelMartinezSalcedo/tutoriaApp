using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.UseCases.Users.Me;

namespace tutoriaBE.UseCases.Users.Promote;
public class PromoteUserHandler: IQueryHandler<PromoteUserQuery, Result<PromotedUserDTO>>
{
  private readonly IRepository<User> _repository;
  public PromoteUserHandler(IRepository<User> repository)
  {
    _repository = repository;
  }

  public async Task<Result<PromotedUserDTO>> Handle(PromoteUserQuery request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.id, cancellationToken);

    if (user == null)
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.id),
        ErrorMessage = "User not found."
      });
    }

    var promoteResult = user.Promote();

    if (!promoteResult.IsSuccess)
    {
      return Result.Invalid(promoteResult.ValidationErrors);
    }

    await _repository.UpdateAsync(user, cancellationToken);

    var userDto = new PromotedUserDTO(
        user.FirstName,
        user.LastName,
        user.Role
    );

    return Result.Success(userDto);
  }

}
