using System;

namespace Expense.API.Application.Model.Response
{
    public class ExpenseResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Double Value { get; set; }
        public Boolean PaidOut { get; set; }
        public DateTime Created { get; set; }
        public DateTime DatePaidOut { get; set; }
    }
}
