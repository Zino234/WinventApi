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
    public class AdminRepositries : IAdminRepository
    {
        private readonly WinventDbContext _context;
        public AdminRepositries(WinventDbContext context)
        {
            _context = context;
        }
        public async Task<Admin> AdminLogin(Admin adminLogin)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x=> x.AdminUsername == adminLogin.AdminUsername && x.AdminPassword == adminLogin.AdminPassword);
            if (admin == null)
            {
                return null;
            }

            return admin;
        }

        
    }
}
