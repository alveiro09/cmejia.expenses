using ExpenseManagement.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.Domain.Infraestructure.EntityConfiguration
{
    internal class ExpenseTypeEntityConfiguration : IEntityTypeConfiguration<ExpenseType>
    {
        public void Configure(EntityTypeBuilder<ExpenseType> builder)
        {
            builder.ToTable("expensesType");

            builder.HasKey(item => item.Id);

            builder.Property(item => item.Name)
                   .HasColumnName("name")
                   .IsRequired();           
        }
    }
}
