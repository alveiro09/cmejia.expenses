using ExpenseManagement.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.Domain.Infraestructure.EntityConfiguration
{
    internal class ExpenseStatusEntityConfiguration : IEntityTypeConfiguration<ExpenseStatus>
    {
        public void Configure(EntityTypeBuilder<ExpenseStatus> builder)
        {
            builder.ToTable("expensesStatus");

            builder.HasKey(item => item.Id);

            builder.Property(item => item.Name)
                   .HasColumnName("name")
                   .IsRequired();           
        }
    }
}
