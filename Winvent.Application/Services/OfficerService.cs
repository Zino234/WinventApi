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
    public class OfficerService : IOfficerService
    {
        private readonly IOfficerRepository _officerRepository;
        public OfficerService(IOfficerRepository officerRepository)
        {
            _officerRepository = officerRepository;
        }
        public async Task<Officer> CreateOfficer(Officer newOfficer)
        {
            var officer = await _officerRepository.CreateOfficer(newOfficer);
            return officer;
        }

       
        public async Task<Officer> OfficerLogin(Officer officerLogin)
        {
            var login = await _officerRepository.OfficerLogin(officerLogin);
            return login;
        }

        public async Task<Officer> GetOfficerById(Guid id)
        {
            var res = await _officerRepository.GetOfficerById(id);
            return res;
        }

        public async Task<Officer> UpdateOfficer(Officer office)
        {
            return await _officerRepository.UpdateOfficer(office);
        }

        public async Task<List<Officer>> GetAllOfficers()
        {
            return await _officerRepository.GetAllOfficers();
        }

        public async Task<Officer> DeleteOfficerById(Guid id)
        {
            return await _officerRepository.DeleteOfficerById(id); 
        }
    }
}
