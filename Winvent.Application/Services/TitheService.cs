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
    public class TitheService : ITitheService
    {
        private readonly ITitheRepository _titheRepository;
        public TitheService(ITitheRepository titheRepository)
        {
            _titheRepository = titheRepository;
        }
        public async Task<Tithe> AddTithe(Tithe newTithe)
        {
            var res = await _titheRepository.AddTithe(newTithe);
            return res;
        }

        public async Task<List<Tithe>> GetAllTithes()
        {
           var rex =  await _titheRepository.GetAllTithes();
            return rex;
        }

        public async Task<Tithe> UpdateTithe(Tithe tithe)
        {
            var res = await _titheRepository.UpdateTithe(tithe);
            return res;
        }

        public async Task<Tithe> GetTitheById(Guid id)
        {
          return await  _titheRepository.GetTitheById(id);
        }

        public async Task<Tithe> DeleteTitheById(Guid Id)
        {
            return await _titheRepository.DeleteTitheById(Id);
        }
    }
}
