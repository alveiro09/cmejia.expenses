using ExpenseManagement.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.Domain.Infraestructure.EntityConfiguration
{
    internal class ExpenseEntityConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("expenses");

            builder.HasKey(item => item.Id);

            builder.Property(item => item.Description)
                   .HasColumnName("description")
                   .IsRequired();

            builder.Property(item => item.Name)
                   .HasColumnName("name")
                   .IsRequired();

            builder.Property(item => item.PaidOut)
                 .HasColumnName("paidout");

            builder.Property(item => item.DatePaidOut)
                .HasColumnName("datepaidout");

            builder.Property(item => item.Value)
                     .HasColumnName("value")
                     .IsRequired();

            builder.Property(item => item.Created)
            .HasColumnName("created")
            .IsRequired()
            .ValueGeneratedOnAdd();

            builder.HasOne(item => item.ExpenseType)
                  .WithMany(item => item.Expenses)
                  .HasForeignKey(item => item.IdExpenseType);

            builder.Property(item => item.UserNameOwner)
                 .HasColumnName("usernameowner");

            builder.Property(item => item.ExpirationDate)
                     .HasColumnName("expirationdate")
                     .IsRequired();
        }
    }
}
