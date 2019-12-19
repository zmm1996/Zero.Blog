using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

using Zero.Core.Entities;

namespace Zero.Infrastructure.DataBase
{
    public class Sys_RoleMap : IEntityTypeConfiguration<Sys_Role>
    {
        

        public void Configure(EntityTypeBuilder<Sys_Role> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name)
                .HasMaxLength(50);

            builder.Property(t => t.Description)
                .HasMaxLength(500);

            builder.Property(t => t.CreatedByUserName)
                .HasMaxLength(128);

            builder.Property(t => t.ModifiedByUserName)
                .HasMaxLength(128);

            // Table & Column Mappings
            builder.ToTable("Sys_Role");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.Status).HasColumnName("Status");
            builder.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            builder.Property(t => t.IsSuperAdministrator).HasColumnName("IsSuperAdministrator");
            builder.Property(t => t.IsBuiltin).HasColumnName("IsBuiltin");
            builder.Property(t => t.CreatedTime).HasColumnName("CreatedTime");
            builder.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            builder.Property(t => t.CreatedByUserName).HasColumnName("CreatedByUserName");
            builder.Property(t => t.ModifiedTime).HasColumnName("ModifiedTime");
            builder.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");
            builder.Property(t => t.ModifiedByUserName).HasColumnName("ModifiedByUserName");
        }
    }
}
