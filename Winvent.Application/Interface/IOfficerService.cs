using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Domain.Models;

namespace Winvent.Application.Interface
{
    public interface IOfficerService
    {
        Task<Officer> CreateOfficer(Officer newOfficer);
        Task<Officer> OfficerLogin(Officer officerLogin);
        Task<Officer> GetOfficerById(Guid OfficerId);
       Task<Officer> UpdateOfficer(Officer office);
        Task<List<Officer>> GetAllOfficers();
        Task<Officer> DeleteOfficerById(Guid id);


    }
}
