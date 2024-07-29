using Netjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

[InjectAsScoped]
public interface IServerTokenService
{
    string Generate(string serverId);
}
