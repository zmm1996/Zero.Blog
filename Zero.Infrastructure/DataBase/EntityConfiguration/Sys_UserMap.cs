using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zero.Core.Entities;

namespace Zero.Infrastructure.DataBase
{
    public class Sys_UserMap : IEntityTypeConfiguration<Sys_User>
    {
     

        public void Configure(EntityTypeBuilder<Sys_User> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.LoginName)
                .HasMaxLength(50);

            builder.Property(t => t.DisplayName)
                .HasMaxLength(50);

            builder.Property(t => t.Password)
                .HasMaxLength(128);

            builder.Property(t => t.Avatar)
                .HasMaxLength(255);

            builder.Property(t => t.Description)
                .HasMaxLength(255);

            builder.Property(t => t.CreatedByUserName)
                .HasMaxLength(128);

            builder.Property(t => t.ModifiedByUserName)
                .HasMaxLength(128);

            // Table & Column Mappings
            builder.ToTable("Sys_User");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.LoginName).HasColumnName("LoginName");
            builder.Property(t => t.DisplayName).HasColumnName("DisplayName");
            builder.Property(t => t.Password).HasColumnName("Password");
            builder.Property(t => t.Avatar).HasColumnName("Avatar");
            builder.Property(t => t.UserType).HasColumnName("UserType");
            builder.Property(t => t.IsLocked).HasColumnName("IsLocked");
            builder.Property(t => t.Status).HasColumnName("Status");
            builder.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.CreatedTime).HasColumnName("CreatedTime");
            builder.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            builder.Property(t => t.CreatedByUserName).HasColumnName("CreatedByUserName");
            builder.Property(t => t.ModifiedTime).HasColumnName("ModifiedTime");
            builder.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");
            builder.Property(t => t.ModifiedByUserName).HasColumnName("ModifiedByUserName");
        }
    }
}
