using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Auth;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Auth;

public record RegisterUserCommand(RegisterUserRequest RegisterUserRequest) : IRequestWrapper<Unit>;

internal sealed class RegisterUserCommandHandler : IHandlerWrapper<RegisterUserCommand,Unit>
{
    private readonly UserManager<User> _userManager;
    private readonly IRegisterService _registerService;

    public RegisterUserCommandHandler(UserManager<User> userManager, IRegisterService registerService)
    {
        _userManager = userManager;
        _registerService = registerService;
    }
    
    public async Task<IResponse<Unit>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User() 
        { 
            UserName = request.RegisterUserRequest.Username,
            DeviceId = request.RegisterUserRequest.DeviceId,
            GameName = request.RegisterUserRequest.GameName
        };

        var createResult = await _userManager.CreateAsync(user, request.RegisterUserRequest.DeviceId);

        if (createResult.Succeeded) 
        {
            await _registerService.Registrate(user, cancellationToken);
            return Response.Success(Unit.Value);
        }
        else
        {
            return Response.Fail<Unit>(createResult.Errors.Select(error => error.Description).ToList());
        }
    }
}