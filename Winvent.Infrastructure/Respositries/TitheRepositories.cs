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
    public class TitheRepositories : ITitheRepository
    {
        private readonly WinventDbContext _context;
        public TitheRepositories(WinventDbContext context)
        {
            _context = context; 
        }
        public async Task<Tithe> AddTithe(Tithe newTithe)
        {
           await _context.Tithes.AddAsync(newTithe);
           await  _context.SaveChangesAsync();
            return newTithe;
        }

        public async Task<List<Tithe>> GetAllTithes()
        {
            var res = await _context.Tithes.ToListAsync();
            return res;
        }

        public async Task<Tithe> UpdateTithe(Tithe tithe)
        {
            var result = await _context.Tithes.FirstOrDefaultAsync(x => x.TitheId == tithe.TitheId);
            if(result == null)
            {
                _context.Tithes.Add(tithe);
            }
            else
            {
                _context.Entry(tithe).State = EntityState.Modified;
                result.TitheCollectedAt = tithe.TitheCollectedAt;
                result.TitheGivenBy = tithe.TitheGivenBy;
                result.TitheName = tithe.TitheName;
                result.TitheCollectedAt = tithe.TitheCollectedAt;
                result.ServiceType = tithe.ServiceType;
            }
            await _context.SaveChangesAsync();

            return result;
        }
        public async Task<Tithe> GetTitheById(Guid id)
        {
          var res =  await _context.Tithes.FindAsync(id);
            return res;
        }

        public async Task<Tithe> DeleteTitheById(Guid Id)
        {
            var dorder = await _context.Tithes.Where(x => x.TitheId == Id).FirstOrDefaultAsync();
            if (dorder != null)
            {
                _context.Tithes.Remove(dorder);
                _context.Entry(dorder).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
            return dorder;
        }
    }
}
