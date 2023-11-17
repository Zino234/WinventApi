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
    public class OfficerRepositories : IOfficerRepository
    {
        private readonly WinventDbContext _context;
        public OfficerRepositories(WinventDbContext context)
        {
               _context = context;
        }
        public async Task<Officer> CreateOfficer(Officer newOfficer)
        {
            _context.Officers.Add(newOfficer);
            await _context.SaveChangesAsync();
            return newOfficer;
        }

        public async Task<Officer> FindOfficer(Guid OfficerId)
        {
            return await _context.Officers.FindAsync(OfficerId);
        }

        public async Task<Officer> OfficerLogin(Officer officerLogin)
        {
            var officer = await _context.Officers.Where(x=> x.OfficerUsername == officerLogin.OfficerUsername).FirstOrDefaultAsync();
            if (officer != null)
            {
                if (BCrypt.Net.BCrypt.Verify(officerLogin.OfficerPassword, officer.OfficerPassword))
                {
                    return (officer);
                }
                return null;
            }

            return officer;
        }

        public async Task<Officer> GetOfficerById(Guid id)
        {
            return await _context.Officers.FindAsync(id);
        }

        public async Task<Officer> UpdateOfficer(Officer office)
        {
          var res = await _context.Officers.FirstOrDefaultAsync(x => x.OficcerId== office.OficcerId);
            if(res == null)
            {
                return null;
            }

            else
            {
                _context.Entry(office).State = EntityState.Modified;
                res.OfficerLastname = office.OfficerLastname;
                res.OfficerPhone = office.OfficerPhone;
                res.OfficerUsername = office.OfficerUsername;
                res.OfficerPassword = office.OfficerPassword;
                res.OfficerFirstname = office.OfficerFirstname;
                res.OfficerEmail = res.OfficerEmail;
               
            }
             await _context.SaveChangesAsync();
            return res;

        }

        public async Task<List<Officer>> GetAllOfficers()
        {
           var res = await _context.Officers.ToListAsync();
            return res;
        }

        public async Task<Officer> DeleteOfficerById(Guid id)
        {
            var dorder = await _context.Officers.Where(x => x.OficcerId == id).FirstOrDefaultAsync();
            if (dorder != null)
            {
                _context.Officers.Remove(dorder);
                _context.Entry(dorder).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
            return dorder;
        }
    }
}
