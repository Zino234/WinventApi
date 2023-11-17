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
    public class OfferingService : IOfferingService
    {
        private readonly IOfferingRepository _offeringRepository;
        public OfferingService(IOfferingRepository offeringRepository)
        {
            _offeringRepository = offeringRepository;
        }
        public async Task<Offering> CreateOffering(Offering newOffering)
        {
            var offering = await _offeringRepository.CreateOffering(newOffering);
            return offering;
        }

        public async Task<List<Offering>> GetAllOfferings()
        {
            var offering = await _offeringRepository.GetAllOfferings();
            return offering;
        }

        public async Task<Offering> UpdateOffering(Offering offering)
        {
           var off = await _offeringRepository.UpdateOffering(offering);
            return off;
        }

        public async Task<Offering> GetOfferingById(Guid id)
        {
            return await _offeringRepository.GetOfferingById(id);
        }

        public async Task<Offering> DeleteOfferingById(Guid Id)
        {
           return await _offeringRepository.DeleteOfferingById(Id);
        }
    }
}
