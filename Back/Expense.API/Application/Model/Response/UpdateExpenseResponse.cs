using System;

namespace Expense.API.Application.Model.Response
{
    public class UpdateExpenseResponse
    {
        public Guid Id { get; set; }
        public bool Updated { get; set; }
        public string Message { get; set; }
    }
}
