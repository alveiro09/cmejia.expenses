using Domain.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Infraestructure.EntityConfiguration;
using UserManagement.Domain.Model;

namespace UserManagement.Domain.Infraestructure
{
    public class UserManagementContext : DbContext, IDbContext
    {
        #region DB Sets
        public DbSet<User> Users { get; set; }
        #endregion

        #region Configuration

        public UserManagementContext(DbContextOptions<UserManagementContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
        #endregion
    }
}
