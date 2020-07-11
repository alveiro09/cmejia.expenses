using Domain.Core.Contracts;
using ExpenseManagement.Domain.Infraestructure.EntityConfiguration;
using ExpenseManagement.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Domain.Infraestructure
{
    public class ExpenseManagementContext : DbContext, IDbContext
    {
        #region DB Sets
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<ExpenseStatus> ExpenseStatus { get; set; }
        public DbSet<ExpenseRecurrenceType> ExpenseRecurrenceTypes { get; set; }
        #endregion

        #region Configuration

        public ExpenseManagementContext(DbContextOptions<ExpenseManagementContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseStatusEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseRecurrenceTypeEntityConfiguration());
        }
        #endregion
    }
}
