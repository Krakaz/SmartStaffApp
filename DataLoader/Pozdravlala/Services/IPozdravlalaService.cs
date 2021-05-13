using DataLoader.Pozdravlala.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.Pozdravlala.Services
{
    public interface IPozdravlalaService
    {
        Task<string> GetCongratulationAsync(CongratulationRequest requestData, CancellationToken cancellationToken);
    }
}
