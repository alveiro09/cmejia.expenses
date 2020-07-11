using ExpenseManagement.Domain.Model;
using System.Collections.Generic;

namespace Expense.API.Infraestructure.Seeds
{
    internal class ExpenseRecurrenceTypeSeed
    {
        internal static IEnumerable<ExpenseRecurrenceType> GetDefaultExpenseRecurrenceType()
        {
            return new List<ExpenseRecurrenceType>() {
                new ExpenseRecurrenceType() { Id = 1, Name = "Hour" },
                new ExpenseRecurrenceType() { Id = 2, Name = "Day" },
                new ExpenseRecurrenceType() { Id = 3, Name = "Month" },
                new ExpenseRecurrenceType() { Id = 4, Name = "Year" }
        };
        }
    }
}
