using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Domain.Models;

namespace Winvent.Application.Repositries
{
    public interface IExpenseRespository
    {
        Task<Expense> AddExpense(Expense newExpense);
        Task<Expense> UpdateExpense(Expense expense);
        Task<List<Expense>> GetAllExpenses();
        Task<Expense> GetExpenseById(Guid Id);
        Task<Expense> DeleteExpenseById(Guid id);
    }
}
