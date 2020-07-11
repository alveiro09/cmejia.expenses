using Domain.Core.Base;
using System.Collections.Generic;

namespace ExpenseManagement.Domain.Model
{
    public class ExpenseType : Entity<int>
    {
        #region Properties
        public string Name { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        #endregion
    }
}
