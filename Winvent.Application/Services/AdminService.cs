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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<Admin> AdminLogin(Admin adminLogin)
        {
            var login = await _adminRepository.AdminLogin(adminLogin);
            return login;
        }

        
    }
}
