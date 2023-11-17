using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Domain.Models;

namespace Winvent.Application.Interface
{
    public interface IAdminService
    {
        Task<Admin> AdminLogin(Admin adminLogin);
        
    }
}
