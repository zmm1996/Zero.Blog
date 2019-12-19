using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zero.Core.Entities;

namespace Zero.Infrastructure.DataBase
{
    public class Sys_PermissionMap : IEntityTypeConfiguration<Sys_Permission>
    {
        public void Configure(EntityTypeBuilder<Sys_Permission> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name)
                .HasMaxLength(50);

            builder.Property(t => t.ActionCode)
                .HasMaxLength(80);

            builder.Property(t => t.CreatedByUserName)
                .HasMaxLength(128);

            builder.Property(t => t.ModifiedByUserName)
                .HasMaxLength(128);

            // Table & Column Mappings
            builder.ToTable("Sys_Permission");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.MenuId).HasColumnName("MenuId");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.ActionCode).HasColumnName("ActionCode");
            builder.Property(t => t.Icon).HasColumnName("Icon");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.Status).HasColumnName("Status");
            builder.Property(t => t.IsDelete).HasColumnName("IsDelete");
            builder.Property(t => t.Type).HasColumnName("Type");
            builder.Property(t => t.CreatedTime).HasColumnName("CreatedTime");
            builder.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            builder.Property(t => t.CreatedByUserName).HasColumnName("CreatedByUserName");
            builder.Property(t => t.ModifiedTime).HasColumnName("ModifiedTime");
            builder.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");
            builder.Property(t => t.ModifiedByUserName).HasColumnName("ModifiedByUserName");

        }
    }
}
