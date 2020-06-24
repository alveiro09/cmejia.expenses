using Expense.API.Application.Model.Request;
using Expense.API.Application.Model.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expense.API.Application.Contracts
{
    /// <summary>
    /// Service to manager Expenses
    /// </summary>
    public interface IExpenseService
    {
        /// <summary>
        /// Register a new Expense
        /// </summary>
        /// <param name="addExpenseRequest"></param>
        AddExpenseResponse AddExpense(AddExpenseRequest addExpenseRequest);

        /// <summary>
        /// Get Expense
        /// </summary>
        /// <returns></returns>
        Task<ExpenseResponse> GetExpense(Guid id);

        /// <summary>
        /// Get Expenses
        /// </summary>
        /// <returns></returns>
        Task<List<ExpenseResponse>> GetExpenses();

    }
}
