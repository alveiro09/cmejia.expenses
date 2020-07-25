using System;

namespace Expense.API.Application.Model.Response
{
    public class ExpenseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Double Value { get; set; }
        public Boolean PaidOut { get; set; }
        public string Created { get; set; }
        public string DatePaidOut { get; set; }
        public int IdExpenseType { get; set; }
        public string UserNameOwner { get; set; }
        public string ExpirationDate { get; set; }
        public int IdExpenseStatus { get; set; }
        public int IdExpenseRecurrenceType { get; set; }
    }
}
