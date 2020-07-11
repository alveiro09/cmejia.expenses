using ExpenseManagement.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.Domain.Infraestructure.EntityConfiguration
{
    internal class ExpenseRecurrenceTypeEntityConfiguration : IEntityTypeConfiguration<ExpenseRecurrenceType>
    {
        public void Configure(EntityTypeBuilder<ExpenseRecurrenceType> builder)
        {
            builder.ToTable("expensesRecurrenceType");

            builder.HasKey(item => item.Id);

            builder.Property(item => item.Name)
                   .HasColumnName("name")
                   .IsRequired();           
        }
    }
}
