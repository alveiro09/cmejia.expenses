using Domain.Core.Model;
using Expense.API.Application.Contracts;
using Expense.API.Application.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expense.API.Controllers
{
    /// <summary>
    ///  controller to manage Expense 
    /// </summary>

    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _ExpenseService;
        private readonly ILogger<ExpenseController> _logger;

        /// <summary>
        ///  Constructor with the environment and repository dependencies
        /// </summary>
        /// <param name="ExpenseService"></param>
        /// <param name="logger"></param>
        public ExpenseController(IExpenseService ExpenseService, ILogger<ExpenseController> logger)
        {
            _ExpenseService = ExpenseService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new Expense. 
        /// </summary>
        /// <remarks>Endpoint to create a new expense</remarks>
        /// <param name="addExpenseRequest">information about the Expense</param>
        [Microsoft.AspNetCore.Mvc.HttpPost()]
        [Consumes("application/json")]
        [Produces("application/json")]
        public Task<IActionResult> AddExpense([FromBody]AddExpenseRequest addExpenseRequest)
        {
            return _ExpenseService.AddExpense(addExpenseRequest);
        }

        /// <summary>
        /// Get a Expense info. 
        /// </summary>
        /// <remarks>Endpoint to get an expenses</remarks>
        /// <param name="id">Expense id</param>
        [Microsoft.AspNetCore.Mvc.HttpGet("id")]
        [Produces("application/json")]
        public Task<IActionResult> GetExpense(Guid id)
        {
            return _ExpenseService.GetExpense(id);
        }

        /// <summary>
        /// Get list of Expense info. 
        /// </summary>
        /// <remarks>Endpoint to get all the expenses</remarks>
        [Microsoft.AspNetCore.Mvc.HttpGet()]
        [Produces("application/json")]
        public Task<IActionResult> GetExpenses()
        {
            return _ExpenseService.GetExpenses();
        }

        /// <summary>
        /// Get a Expenses info. 
        /// </summary>
        /// <remarks>Endpoint to get an expenses</remarks>
        /// <param name="usernameOwner">Expense id</param>
        [Microsoft.AspNetCore.Mvc.HttpGet("usernameOwner")]
        [Produces("application/json")]
        public Task<IActionResult> GetExpenses(string usernameOwner)
        {
            return _ExpenseService.GetExpenses(usernameOwner);
        }

        /// <summary>
        /// Update a Expenses by Id. 
        /// </summary>
        /// <remarks>Endpoint to get an expenses</remarks>
        /// <param name="id">Expense id</param>
        /// <param name="patchDtos">Expense info</param>
        [HttpPatch("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateExpense([FromRoute] Guid id, [FromBody] List<PatchDto> patchDtos)
        {
            return await _ExpenseService.UpdateExpense(id, patchDtos);
        }
    }
}
