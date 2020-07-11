using Domain.Core.Base;

namespace ExpenseManagement.Domain.Model
{
    public class ExpenseRecurrenceType : Entity<int>
    {
        #region Properties
        public string Name { get; set; }
        #endregion
    }
}
