using Domain.Core.Contracts;
using ExpenseManagement.Domain.Model;

namespace ExpenseManagement.Domain.Repositories
{
    public interface IExpenseRepository : IRepository<Expense> { }
}
