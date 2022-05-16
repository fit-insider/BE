using FI.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FI.Data.Configurations.Users
{
    public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.Property(ud => ud.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ud => ud.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ud => ud.Password)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}