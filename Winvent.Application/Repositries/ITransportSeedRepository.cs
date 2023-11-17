using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Domain.Models;

namespace Winvent.Application.Repositries
{
    public interface ITransportSeedRepository
    {
        Task<TransportSeed> AddTransportSeed(TransportSeed newTransportSeed);
        Task<TransportSeed> UpdateTransportSeed(TransportSeed transportSeed);
        Task<List<TransportSeed>> GetAllTransportSeeds();
        Task<TransportSeed> GetTransportSeedById(Guid Id);
        Task<TransportSeed> DeleteTransportSeedById(Guid Id);
    }
}
