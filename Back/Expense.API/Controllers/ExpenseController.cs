using Expense.API.Application.Contracts;
using Expense.API.Application.Model.Request;
using Expense.API.Application.Model.Response;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
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
        /// <remarks>Endpoint to create a new tenant</remarks>
        /// <param name="addExpenseRequest">information about the Expense</param>
        [HttpPost()]
        [Consumes("application/json")]
        public AddExpenseResponse AddExpense([FromBody]AddExpenseRequest addExpenseRequest)
        {
            return _ExpenseService.AddExpense(addExpenseRequest);
        }

        /// <summary>
        /// Get a Expense info. 
        /// </summary>
        /// <remarks>Endpoint to create a new tenant</remarks>
        /// <param name="id">Expense id</param>
        [HttpGet("id")]
        [Consumes("application/json")]
        public Task<ExpenseResponse> GetExpense(Guid id)
        {
            return _ExpenseService.GetExpense(id);
        }

        /// <summary>
        /// Get list of Expense info. 
        /// </summary>
        /// <remarks>Endpoint to create a new tenant</remarks>
        /// <param name="id">Expense id</param>
        [HttpGet("id")]
        [Consumes("application/json")]
        public Task<List<ExpenseResponse>> GetExpenses()
        {
            return _ExpenseService.GetExpenses();
        }
    }
}
