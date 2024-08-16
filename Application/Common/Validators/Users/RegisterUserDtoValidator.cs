using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Auth;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Validators.Users;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserRequest>
{
    private readonly UserManager<User> _userManager;

    public RegisterUserDtoValidator(UserManager<User> userManager)
    {
        _userManager = userManager;
        //RuleFor(x => x.Email)
        //    .NotEmpty().NotNull().EmailAddress()
        //    .MustAsync(UniqueAsync).WithMessage("User with this email already exists");

        //RuleFor(x => x.Password)
        //    .NotEmpty().NotNull();
            
        //RuleFor(x => x.ConfirmPassword)
        //    .NotEmpty().NotNull()
        //    .Matches(x=>x.Password);

       // RuleFor(x => x.DeviceId).NotEmpty().NotNull().Must(UniqueDevice).WithMessage("User with same device id exist");

    }

    private bool UniqueDevice(string deviceId)
    {
        var any = _userManager.Users.Any(x => x.DeviceId == deviceId);
        return !any;
    }

    private async Task<bool> UniqueAsync(string email, CancellationToken cancellationToken)
    {
        var any = await _userManager.Users.AnyAsync(x => x.Email == email, cancellationToken);
        return !any;
    }
}