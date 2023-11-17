using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Application.Repositries;
using Winvent.Domain.Models;
using Winvent.Infrastructure.Data;

namespace Winvent.Infrastructure.Respositries
{
    public class OfferingRepositories : IOfferingRepository
    {
        private readonly WinventDbContext _context;
        public OfferingRepositories(WinventDbContext context)
        {
            _context = context;
        }
        public async Task<Offering> CreateOffering(Offering newOffering)
        {
            await _context.Offerings.AddAsync(newOffering);
            await _context.SaveChangesAsync();
            return newOffering;
        }

        public async Task<List<Offering>> GetAllOfferings()
        {
            return await _context.Offerings.ToListAsync();
        }

        public async Task<Offering> UpdateOffering(Offering offering)
        {
            var result = await _context.Offerings.FirstOrDefaultAsync(x => x.OfferingId == offering.OfferingId);
            if (result == null)
            {
                _context.Offerings.Add(offering);
            }
            else
            {
                _context.Entry(offering).State = EntityState.Modified;
                result.OfferingAmount = offering.OfferingAmount;
                result.OfferingCollectedAt = offering.OfferingCollectedAt;
                result.ServiceType = offering.ServiceType;
                
            }

         await _context.SaveChangesAsync();
            return result;
           
        }
        public async Task<Offering> GetOfferingById(Guid id)
        {
            return await _context.Offerings.FindAsync(id);
        }

        public async Task<Offering> DeleteOfferingById(Guid Id)
        {
            var dorder = await _context.Offerings.Where(x => x.OfferingId == Id).FirstOrDefaultAsync();
            if (dorder != null)
            {
                _context.Offerings.Remove(dorder);
                _context.Entry(dorder).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
            return dorder;
        }
    }
}
