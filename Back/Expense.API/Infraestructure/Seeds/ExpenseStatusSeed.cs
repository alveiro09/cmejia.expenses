using ExpenseManagement.Domain.Model;
using System.Collections.Generic;

namespace Expense.API.Infraestructure.Seeds
{
    internal class ExpenseStatusSeed
    {
        internal static IEnumerable<ExpenseStatus> GetDefaultExpenseStatus()
        {
            return new List<ExpenseStatus>() {
                new ExpenseStatus() { Id = 1, Name = "Created" },
                new ExpenseStatus() { Id = 2, Name = "Paused" },
                new ExpenseStatus() { Id = 3, Name = "Done" },
                new ExpenseStatus() { Id = 4, Name = "Dismissed" },
                new ExpenseStatus() { Id = 5, Name = "Expired" }
            };
        }
    }
}
