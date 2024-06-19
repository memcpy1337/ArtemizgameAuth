using Domain.Entities;
using Netjection;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;
/// <summary>
/// Interface for authentication.
/// </summary>
[InjectAsScoped]
public interface IRegisterService
{
    Task Registrate(User user, CancellationToken cancellationToken);
}