using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLoader.Maketalents.Services
{
    public interface IAuthService
    {
        Task<string> GetToken();
    }
}
