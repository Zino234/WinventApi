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
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRespository _expenseRepository;
        public ExpenseService(IExpenseRespository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<Expense> AddExpense(Expense newExpense)
        {
            var exp = await _expenseRepository.AddExpense(newExpense);
            return exp;
        }

        public async Task<List<Expense>> GetAllExpenses()
        {
            var exp = await _expenseRepository.GetAllExpenses();
            return exp;
        }

        public async Task<Expense> UpdateExpense(Expense expense)
        {
           return  await _expenseRepository.UpdateExpense(expense);
        }
        public async Task<Expense> GetExpenseById(Guid id)
        {
            return await _expenseRepository.GetExpenseById(id);
        }

        public async Task<Expense> DeleteExpenseById(Guid id)
        {
         return  await _expenseRepository.DeleteExpenseById(id);
        }
    }
}
