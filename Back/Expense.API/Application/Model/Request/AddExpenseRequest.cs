using System;

namespace Expense.API.Application.Model.Request
{
    public class AddExpenseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Double Value { get; set; }
    }
}
