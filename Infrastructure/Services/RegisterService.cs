using Application.Common.Interfaces;
using Domain.Entities;
using MassTransit;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using System;

namespace Infrastructure.Services;
internal sealed class RegisterService : IRegisterService
{
    private readonly IPublishEndpoint _publishEndpoint;
    public RegisterService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Registrate(User user, CancellationToken cancellationToken)
    {
       await _publishEndpoint.Publish(new UserRegisterEvent()
       { 
           Id = user.Id, 
           Name = user.UserName!, 
           CreatedOnUtc = DateTime.UtcNow 
       }, cancellationToken);
    }
}