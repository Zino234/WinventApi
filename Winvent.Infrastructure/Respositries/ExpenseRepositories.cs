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
    public class ExpenseRepositories:IExpenseRespository
    {
        private readonly WinventDbContext _context;
        public ExpenseRepositories(WinventDbContext context)
        {
            _context = context;
        }

        public async Task<Expense> AddExpense(Expense newExpense)
        {
            await _context.Expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();
            return newExpense;
        }

        public async Task<List<Expense>> GetAllExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Expense> UpdateExpense(Expense expense)
        {
            var result = await _context.Expenses.FirstOrDefaultAsync(x => x.ExpenseId == expense.ExpenseId);
            if (result == null)
            {
                return null;
            }

            else
            {
                _context.Entry(expense).State = EntityState.Modified;
                result.ExpenseTitle = expense.ExpenseTitle;
                result.ExpenseAmount = expense.ExpenseAmount;
                result.ExpenseDoneAt = expense.ExpenseDoneAt;
                result.ExpenseDescription = expense.ExpenseDescription;
                result.ServiceType = expense.ServiceType;
               
            }

            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Expense> GetExpenseById(Guid id)
        {
           var exp = await _context.Expenses.FirstOrDefaultAsync(x=> x.ExpenseId == id);
            return exp;
        }

        public async Task<Expense> DeleteExpenseById(Guid id)
        {
            var dorder = await _context.Expenses.Where(x => x.ExpenseId == id).FirstOrDefaultAsync();
            if (dorder != null)
            {
                _context.Expenses.Remove(dorder);
                _context.Entry(dorder).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
            return dorder;
        }
    }
}
