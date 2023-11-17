using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Domain.Models;

namespace Winvent.Application.Repositries
{
    public interface IOfferingRepository
    {
        Task<Offering> CreateOffering(Offering newOffering);
        Task<List<Offering>> GetAllOfferings();
        Task<Offering> UpdateOffering(Offering offering);
        Task<Offering> GetOfferingById(Guid Id);
        Task<Offering> DeleteOfferingById(Guid Id);

    }
}
