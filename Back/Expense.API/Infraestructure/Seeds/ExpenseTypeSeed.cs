using ExpenseManagement.Domain.Model;
using System.Collections.Generic;

namespace Expense.API.Infraestructure.Seeds
{
    internal class ExpenseTypeSeed
    {
        internal static IEnumerable<ExpenseType> GetDefaultExpenses()
        {
            return new List<ExpenseType>() {
                new ExpenseType() { Id = 1, Name = "Fixed" },
                new ExpenseType() { Id = 2, Name = "Variable" },
                new ExpenseType() { Id = 3, Name = "Unexpected" },
                new ExpenseType() { Id = 4, Name = "Ant" },
                new ExpenseType() { Id = 5, Name = "Flexible" },
                new ExpenseType() { Id = 5, Name = "Other" }
        };
        }
    }
}
