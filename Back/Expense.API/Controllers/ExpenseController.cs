using Expense.API.Application.Contracts;
using Expense.API.Application.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        [HttpPost()]
        [Consumes("application/json")]
        public Task<IActionResult> AddExpense([FromBody]AddExpenseRequest addExpenseRequest)
        {
            return _ExpenseService.AddExpense(addExpenseRequest);
        }

        /// <summary>
        /// Get a Expense info. 
        /// </summary>
        /// <remarks>Endpoint to get an expenses</remarks>
        /// <param name="id">Expense id</param>
        [HttpGet("id")]
        [Consumes("application/json")]
        public Task<IActionResult> GetExpense(Guid id)
        {
            return _ExpenseService.GetExpense(id);
        }

        /// <summary>
        /// Get list of Expense info. 
        /// </summary>
        /// <remarks>Endpoint to get all the expenses</remarks>
        [HttpGet()]
        [Consumes("application/json")]
        public Task<IActionResult> GetExpenses()
        {
            return _ExpenseService.GetExpenses();
        }
    }
}
