using System;

namespace Expense.API.Application.Model.Response
{
    public class AddExpenseResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Double Value { get; set; }
        public bool Added { get; set; }
        public string Message { get; set; }
    }
}
