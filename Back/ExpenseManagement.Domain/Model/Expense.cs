﻿using Domain.Core.Base;
using System;

namespace ExpenseManagement.Domain.Model
{
    public class Expense : Entity<Guid>
    {
        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public Double Value { get; set; }
        public Boolean PaidOut { get; set; }
        public DateTime Created { get; set; }
        public DateTime DatePaidOut { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }
        public int IdExpenseType { get; set; }
        public string UserNameOwner { get; set; }
        public DateTime ExpirationDate { get; set; }
        #endregion
        public Expense()
        {
            Created = DateTime.UtcNow;
        }
    }
}
