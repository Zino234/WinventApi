using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Domain.Models;

namespace Winvent.Application.Repositries
{
    public interface IOfficerRepository
    {
        Task<Officer> CreateOfficer(Officer newOfficer);
        Task<Officer> OfficerLogin(Officer officerLogin);
        Task<Officer> FindOfficer(Guid OfficerId);
        Task<Officer> GetOfficerById(Guid Id);
        Task<Officer> UpdateOfficer(Officer office);
        Task<List<Officer>> GetAllOfficers();
        Task<Officer> DeleteOfficerById(Guid id);
    }
}
