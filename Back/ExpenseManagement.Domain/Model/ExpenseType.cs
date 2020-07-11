using Domain.Core.Base;

namespace ExpenseManagement.Domain.Model
{
    public class ExpenseType : Entity<int>
    {
        #region Properties
        public string Name { get; set; }
        #endregion
    }
}
