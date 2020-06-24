using Domain.Core.Base;
using ExpenseManagement.Domain.Model;
using ExpenseManagement.Domain.Repositories;

namespace ExpenseManagement.Domain.Infraestructure.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ExpenseManagementContext context)
            : base(context) { }
    }
}
