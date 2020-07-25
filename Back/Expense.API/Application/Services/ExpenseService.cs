using Domain.Core.Contracts;
using Domain.Core.Model;
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
                IdExpenseStatus = 1,
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
                result.Created = ExpenseToFind.Created.ToString("dddd, dd MMMM yyyy");
                result.DatePaidOut = ExpenseToFind.DatePaidOut.ToString("dddd, dd MMMM yyyy");
                result.Description = ExpenseToFind.Description;
                result.ExpirationDate = ExpenseToFind.ExpirationDate.ToString("dddd, dd MMMM yyyy");
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
                        Created = Expense.Created.ToString("dddd, dd MMMM yyyy"),
                        DatePaidOut = Expense.DatePaidOut.ToString("dddd, dd MMMM yyyy"),
                        Description = Expense.Description,
                        ExpirationDate = Expense.ExpirationDate.ToString("dddd, dd MMMM yyyy"),
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
                        Created = ExpenseToFind.Created.ToString("dddd, dd MMMM yyyy"),
                        DatePaidOut = ExpenseToFind.DatePaidOut.ToString("dddd, dd MMMM yyyy"),
                        Description = ExpenseToFind.Description,
                        ExpirationDate = ExpenseToFind.ExpirationDate.ToString("dddd, dd MMMM yyyy"),
                        IdExpenseType = ExpenseToFind.IdExpenseType,
                        UserNameOwner = ExpenseToFind.UserNameOwner
                    };
                    result.Add(expenseResponse);
                }
                return new OkObjectResult(result);
            }
            else return new NotFoundResult();
        }

        public async Task<IActionResult> UpdateExpense(Guid id, List<PatchDto> patchDtos)
        {
            UpdateExpenseResponse result = new UpdateExpenseResponse() { Message = "", Id = id, Updated = false };
            var expenseToFind = (await _expenseRepository.GetAsync(Expense => Expense.Id.Equals(id))).FirstOrDefault();
            if (expenseToFind != null)
            {
                var update = await _expenseRepository.ApplyPatchAsync(expenseToFind, patchDtos);
                result.Updated = update.Equals(1);
                result.Message = result.Updated ? "" : $"Error updating expense {id}";

                return new OkObjectResult(result);
            }
            else return new NotFoundResult();
        }
    }
}
