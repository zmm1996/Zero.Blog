using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Zero.Core.Entities;

namespace Zero.Infrastructure.DataBase
{
    public class Sys_RolePermissionMap : IEntityTypeConfiguration<Sys_RolePermission>
    {
   

        public void Configure(EntityTypeBuilder<Sys_RolePermission> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            builder.ToTable("Sys_RolePermission");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.RoleId).HasColumnName("RoleId");
            builder.Property(t => t.PermissionId).HasColumnName("PermissionId");
        }
    }
}
