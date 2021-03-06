using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zero.Core.Entities;

namespace Zero.Infrastructure.DataBase
{
    public class Sys_MenuMap : IEntityTypeConfiguration<Sys_Menu>
    {

        public void Configure(EntityTypeBuilder<Sys_Menu> builder)
        {
            //builder.Property(x => x.Author).IsRequired().HasMaxLength(50);
            //builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            //  builder.Property(x => x.Body).IsRequired().HasColumnType("ss");

            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name)
                .HasMaxLength(50);

            builder.Property(t => t.Url)
                .HasMaxLength(255);

            builder.Property(t => t.Alias)
                .HasMaxLength(255);

            builder.Property(t => t.Icon)
                .HasMaxLength(128);

            builder.Property(t => t.ParentName)
                .HasMaxLength(50);

            builder.Property(t => t.Description)
                .HasMaxLength(255);

            builder.Property(t => t.Component)
                .HasMaxLength(255);

            builder.Property(t => t.BeforeCloseFun)
                .HasMaxLength(255);

            builder.Property(t => t.CreatedByUserName)
                .HasMaxLength(128);

            builder.Property(t => t.ModifiedByUserName)
                .HasMaxLength(128);

            // Table & Column Mappings
            builder.ToTable("Sys_Menu");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Url).HasColumnName("Url");
            builder.Property(t => t.Alias).HasColumnName("Alias");
            builder.Property(t => t.Icon).HasColumnName("Icon");
            builder.Property(t => t.ParentId).HasColumnName("ParentId");
            builder.Property(t => t.ParentName).HasColumnName("ParentName");
            builder.Property(t => t.Level).HasColumnName("Level");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.Sort).HasColumnName("Sort");
            builder.Property(t => t.Status).HasColumnName("Status");
            builder.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            builder.Property(t => t.IsDefaultRouter).HasColumnName("IsDefaultRouter");
            builder.Property(t => t.Component).HasColumnName("Component");
            builder.Property(t => t.HideInMenu).HasColumnName("HideInMenu");
            builder.Property(t => t.NotCache).HasColumnName("NotCache");
            builder.Property(t => t.BeforeCloseFun).HasColumnName("BeforeCloseFun");
            builder.Property(t => t.CreatedTime).HasColumnName("CreatedTime");
            builder.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");
            builder.Property(t => t.CreatedByUserName).HasColumnName("CreatedByUserName");
            builder.Property(t => t.ModifiedTime).HasColumnName("ModifiedTime");
            builder.Property(t => t.ModifiedByUserId).HasColumnName("ModifiedByUserId");
            builder.Property(t => t.ModifiedByUserName).HasColumnName("ModifiedByUserName");


        }
    }
}
