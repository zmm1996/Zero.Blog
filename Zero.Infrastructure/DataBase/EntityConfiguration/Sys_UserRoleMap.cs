using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zero.Core.Entities;

namespace Zero.Infrastructure.DataBase
{
    public class Sys_UserRoleMap : IEntityTypeConfiguration<Sys_UserRole>
    {
        

        public void Configure(EntityTypeBuilder<Sys_UserRole> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            builder.ToTable("Sys_UserRole");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.UserId).HasColumnName("UserId");
            builder.Property(t => t.RoleId).HasColumnName("RoleId");
        }
    }
}
