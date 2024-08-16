using Application.Common.Interfaces;
using Contracts.Events.ServerEvents;
using MassTransit;
using System.Threading.Tasks;

namespace Infrastructure.Consumers;

public class TokenServerRequestConsumer : IConsumer<ServerTokenRequest>
{
    private readonly IServerTokenService _serverTokenService;
    public TokenServerRequestConsumer(IServerTokenService serverTokenService)
    {
        _serverTokenService = serverTokenService;
    }

    public async Task Consume(ConsumeContext<ServerTokenRequest> context)
    {
        var token = _serverTokenService.Generate(context.Message.ServerId, context.Message.MatchId);

        await context.RespondAsync(new ServerTokenResponse() { Token = token });
    }
}
