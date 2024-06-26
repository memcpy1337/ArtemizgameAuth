using Application.Common.Interfaces;
using Domain.Entities;
using MassTransit;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Contracts.Events.UserEvents;

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
       var userRegEventData = user.Adapt<UserRegisterEvent>();

       await _publishEndpoint.Publish(userRegEventData, cancellationToken);
    }
}