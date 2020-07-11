using Domain.Core.Model;
using Expense.API.Application.Model.Request;
using Microsoft.AspNetCore.Mvc;
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
        Task<IActionResult> AddExpense(AddExpenseRequest addExpenseRequest);

        /// <summary>
        /// Get Expense
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> GetExpense(Guid id);

        /// <summary>
        /// Get Expenses
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> GetExpenses();

        /// <summary>
        /// Get Expenses by user name owner
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> GetExpenses(string userNameOwner);

        /// <summary>
        /// Update Expenses by id
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> UpdateExpense(Guid id, List<PatchDto> patchDtos);
    }
}
