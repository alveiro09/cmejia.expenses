using Domain.Core.Contracts;
using Expense.API.Application.Contracts;
using Expense.API.Application.Model.Request;
using Expense.API.Application.Model.Response;
using ExpenseManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense.API.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpenseRepository _ExpenseRepository;

        /// <summary>
        /// Contructor with the dependencies required
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="ExpenseRepository"></param>
        public ExpenseService(IUnitOfWork unitOfWork, IExpenseRepository ExpenseRepository)
        {
            _unitOfWork = unitOfWork;
            _ExpenseRepository = ExpenseRepository;
        }
        public AddExpenseResponse AddExpense(AddExpenseRequest addExpenseRequest)
        {
            AddExpenseResponse result = new AddExpenseResponse()
            {
                Name = addExpenseRequest.Name,
                Value = addExpenseRequest.Value,
                Description = addExpenseRequest.Description
            };
            var newExpense = new ExpenseManagement.Domain.Model.Expense()
            {
                Id = Guid.NewGuid(),
                Name = addExpenseRequest.Name,
                Value = addExpenseRequest.Value,
                Description = addExpenseRequest.Description
            };
            try
            {
                _ExpenseRepository.Add(newExpense);
                result.Added = true;
            }
            catch (System.Exception exception)
            {
                result.Message = exception.Message;
            }
            return result;
        }

        public async Task<ExpenseResponse> GetExpense(Guid id)
        {
            ExpenseResponse result = new ExpenseResponse();
            //var Expense = (await _ExpenseRepository.GetAsync(Expense => Expense.Id.Equals(id))).FirstOrDefault();
            var Expense = (await _ExpenseRepository.GetAsync()).FirstOrDefault();
            if (Expense != null)
            {
                result.Name = Expense.Name;
                result.Value = Expense.Value;
                result.PaidOut = Expense.PaidOut;
                result.Created = Expense.Created;
                result.DatePaidOut = Expense.DatePaidOut;
                result.Description = Expense.Description;
            }
            return result;
        }

        public async Task<List<ExpenseResponse>> GetExpenses()
        {
            List<ExpenseResponse> result = new List<ExpenseResponse>();
            var Expenses = await _ExpenseRepository.GetAsync();
            if (Expenses != null)
            {
                foreach (ExpenseManagement.Domain.Model.Expense Expense in Expenses)
                {
                    result.Add(new ExpenseResponse
                    {
                        Name = Expense.Name,
                        Value = Expense.Value,
                        PaidOut = Expense.PaidOut,
                        Created = Expense.Created,
                        DatePaidOut = Expense.DatePaidOut,
                        Description = Expense.Description,
                    });
                }
            }
            return result;
        }
    }
}
