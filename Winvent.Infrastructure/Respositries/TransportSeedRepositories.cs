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
    public class TransportSeedRepositories : ITransportSeedRepository
    {
        private readonly WinventDbContext _context;
        public TransportSeedRepositories(WinventDbContext context)
        {
            _context = context;
        }
        public async Task<TransportSeed> AddTransportSeed(TransportSeed newTransportSeed)
        {
            await _context.TransportSeeds.AddAsync(newTransportSeed);
            await _context.SaveChangesAsync();
            return newTransportSeed;
        }

        public async Task<List<TransportSeed>> GetAllTransportSeeds()
        {
            return await _context.TransportSeeds.ToListAsync(); 
        }

        public async Task<TransportSeed> UpdateTransportSeed(TransportSeed transportSeed)
        {
            var result = await _context.TransportSeeds.FirstOrDefaultAsync(x => x.TransportSeedId == transportSeed.TransportSeedId);
            if (result == null)
            {
                return null;
            }

            else
            {
                _context.Entry(transportSeed).State = EntityState.Modified;
                result.TransportSeedName = transportSeed.TransportSeedName;
                result.TransportSeedGivenBy = transportSeed.TransportSeedGivenBy;
                result.TransportSeedCollectedAt = transportSeed.TransportSeedCollectedAt;
                result.TransportSeedAmount = transportSeed.TransportSeedAmount;
                result.ServiceType = transportSeed.ServiceType;
            }

            await _context.SaveChangesAsync();
            return result;

            
        }
        public async Task<TransportSeed> GetTransportSeedById(Guid id)
        {
            var res = await _context.TransportSeeds.FirstOrDefaultAsync(x=> x.TransportSeedId ==id);
            return res;
        }

        public async Task<TransportSeed> DeleteTransportSeedById(Guid Id)
        {
            var dorder = await _context.TransportSeeds.Where(x => x.TransportSeedId == Id).FirstOrDefaultAsync();
            if (dorder != null)
            {
                _context.TransportSeeds.Remove(dorder);
                _context.Entry(dorder).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
            return dorder;
        }
    }
}
