using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Model;

namespace UserManagement.Domain.Infraestructure.EntityConfiguration
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(item => item.Id);

            builder.Property(item => item.Age)
                   .HasColumnName("age")
                   .IsRequired();

            builder.Property(item => item.Created)
                   .HasColumnName("created")
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(item => item.Email)
                   .HasColumnName("email")
                   .IsRequired();

            builder.Property(item => item.FirstName)
                   .HasColumnName("firstname")
                   .IsRequired();

            builder.Property(item => item.IdentityDocument)
                   .HasColumnName("identitydocument")
                   .IsRequired();

            builder.Property(item => item.IdentityNumber)
                   .HasColumnName("identitynumber")
                   .IsRequired();

            builder.Property(item => item.Password)
                   .HasColumnName("password")
                   .IsRequired();

            builder.Property(item => item.SecondEmail)
                   .HasColumnName("secondemail");

            builder.Property(item => item.SecondName)
                   .HasColumnName("secondname");

            builder.Property(item => item.UserName)
                   .HasColumnName("username")
                   .IsRequired();
        }
    }
}
