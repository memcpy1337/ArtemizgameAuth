using Application.Common.DTOs.Auth;
using FluentValidation;

namespace Application.Common.Validators.Auth;

public class LoginUserDtoValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserDtoValidator()
    {
        RuleFor(x => x.DeviceId)
            .NotEmpty().NotNull();

        RuleFor(x => x.GameName)
            .NotEmpty()
            .NotNull();
    }
}