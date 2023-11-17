using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Application.Interface;
using Winvent.Application.Repositries;
using Winvent.Domain.Models;

namespace Winvent.Application.Services
{
    public class TransportSeedService : ITransportSeedService
    {
        private readonly ITransportSeedRepository _transportSeedRepository;
        public TransportSeedService(ITransportSeedRepository transportSeedRepository)
        {
            _transportSeedRepository = transportSeedRepository;
        }
        public Task<TransportSeed> AddTransportSeed(TransportSeed newTransportSeed)
        {
            return _transportSeedRepository.AddTransportSeed(newTransportSeed);
        }

        public async Task<List<TransportSeed>> GetAllTransportSeeds()
        {
            return await _transportSeedRepository.GetAllTransportSeeds();
        }

        public async Task<TransportSeed> UpdateTransportSeed(TransportSeed transportSeed)
        {
           return await _transportSeedRepository.UpdateTransportSeed(transportSeed);
        }
        public async Task<TransportSeed> GetTransportSeedById(Guid id)
        {
            return await _transportSeedRepository.GetTransportSeedById(id);
        }

        public async Task<TransportSeed> DeleteTransportSeedById(Guid Id)
        {
            return await _transportSeedRepository.DeleteTransportSeedById(Id);
        }
    }
}
