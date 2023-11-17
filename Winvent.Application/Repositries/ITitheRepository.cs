using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Domain.Models;

namespace Winvent.Application.Repositries
{
    public interface ITitheRepository
    {
        Task<Tithe> AddTithe(Tithe newTithe);
        Task<Tithe> UpdateTithe(Tithe tithe);
        Task<List<Tithe>> GetAllTithes();
        Task<Tithe> GetTitheById(Guid Id);
        Task<Tithe> DeleteTitheById(Guid Id);
    }
}
