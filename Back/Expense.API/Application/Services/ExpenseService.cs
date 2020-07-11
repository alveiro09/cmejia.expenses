using Domain.Core.Contracts;
using Expense.API.Application.Contracts;
using Expense.API.Application.Model.Request;
using Expense.API.Application.Model.Response;
using ExpenseManagement.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense.API.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpenseRepository _expenseRepositoryGeneric;
        private readonly IRepository<ExpenseManagement.Domain.Model.Expense> _expenseRepository;

        /// <summary>
        /// Contructor with the dependencies required
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="ExpenseRepositexpenseRepositoryGenericoryGeneric"></param>
        public ExpenseService(IUnitOfWork unitOfWork, IExpenseRepository expenseRepositoryGeneric)
        {
            _unitOfWork = unitOfWork;
            _expenseRepositoryGeneric = expenseRepositoryGeneric;
            _expenseRepository = _unitOfWork.GetRepository<ExpenseManagement.Domain.Model.Expense>();
        }
        public async Task<IActionResult> AddExpense(AddExpenseRequest addExpenseRequest)
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
                Description = addExpenseRequest.Description,
                UserNameOwner = addExpenseRequest.UserNameOwner,
                IdExpenseType = addExpenseRequest.IdExpenseType,
                ExpirationDate = addExpenseRequest.ExpirationDate,
                IdExpenseStatus = addExpenseRequest.IdExpenseStatus,
                IdExpenseRecurrenceType = addExpenseRequest.IdExpenseRecurrenceType
            };
            try
            {
                _expenseRepository.Add(newExpense);
                var commit = _unitOfWork.Commit();
                result.Added = commit > 0;
            }
            catch (System.Exception exception)
            {
                result.Message = exception.Message;
            }
            return new OkObjectResult(result);
        }

        public async Task<IActionResult> GetExpense(Guid id)
        {
            ExpenseResponse result = new ExpenseResponse();
            var ExpenseToFind = (await _expenseRepository.GetAsync(Expense => Expense.Id.Equals(id))).FirstOrDefault();
            if (ExpenseToFind != null)
            {
                result.Id = ExpenseToFind.Id;
                result.Name = ExpenseToFind.Name;
                result.Value = ExpenseToFind.Value;
                result.PaidOut = ExpenseToFind.PaidOut;
                result.Created = ExpenseToFind.Created;
                result.DatePaidOut = ExpenseToFind.DatePaidOut;
                result.Description = ExpenseToFind.Description;
                result.ExpirationDate = ExpenseToFind.ExpirationDate;
                result.IdExpenseType = ExpenseToFind.IdExpenseType;
                result.UserNameOwner = ExpenseToFind.UserNameOwner;
                result.IdExpenseStatus = ExpenseToFind.IdExpenseStatus;
                result.IdExpenseRecurrenceType = ExpenseToFind.IdExpenseRecurrenceType;
                return new OkObjectResult(result);
            }
            else return new NotFoundResult();
        }

        public async Task<IActionResult> GetExpenses()
        {
            List<ExpenseResponse> result = new List<ExpenseResponse>();
            var Expenses = await _expenseRepository.GetAsync();
            if (Expenses != null)
            {
                foreach (ExpenseManagement.Domain.Model.Expense Expense in Expenses)
                {
                    result.Add(new ExpenseResponse
                    {
                        Id = Expense.Id,
                        Name = Expense.Name,
                        Value = Expense.Value,
                        PaidOut = Expense.PaidOut,
                        Created = Expense.Created,
                        DatePaidOut = Expense.DatePaidOut,
                        Description = Expense.Description,
                        ExpirationDate = Expense.ExpirationDate,
                        IdExpenseType = Expense.IdExpenseType,
                        UserNameOwner = Expense.UserNameOwner,
                        IdExpenseStatus = Expense.IdExpenseStatus,
                        IdExpenseRecurrenceType = Expense.IdExpenseRecurrenceType,
                    });
                }
            }
            return new OkObjectResult(result);
        }

        public async Task<IActionResult> GetExpenses(string userNameOwner)
        {
            List<ExpenseResponse> result = new List<ExpenseResponse>();
            var ExpensesToFind = await _expenseRepository.GetAsync(Expense => Expense.UserNameOwner.ToLower().Equals(userNameOwner));
            if (ExpensesToFind != null && ExpensesToFind.Any())
            {
                foreach (var ExpenseToFind in ExpensesToFind)
                {
                    var expenseResponse = new ExpenseResponse()
                    {
                        Id = ExpenseToFind.Id,
                        Name = ExpenseToFind.Name,
                        Value = ExpenseToFind.Value,
                        PaidOut = ExpenseToFind.PaidOut,
                        Created = ExpenseToFind.Created,
                        DatePaidOut = ExpenseToFind.DatePaidOut,
                        Description = ExpenseToFind.Description,
                        ExpirationDate = ExpenseToFind.ExpirationDate,
                        IdExpenseType = ExpenseToFind.IdExpenseType,
                        UserNameOwner = ExpenseToFind.UserNameOwner
                    };
                    result.Add(expenseResponse);
                }
                return new OkObjectResult(result);
            }
            else return new NotFoundResult();
        }
    }
}
