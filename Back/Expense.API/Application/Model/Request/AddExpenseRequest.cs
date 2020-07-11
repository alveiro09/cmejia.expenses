using System;

namespace Expense.API.Application.Model.Request
{
    public class AddExpenseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Double Value { get; set; }
        public int IdExpenseType { get; set; }        
        public string UserNameOwner { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int IdExpenseStatus { get; set; }
        public int IdExpenseRecurrenceType { get; set; }
    }
}
