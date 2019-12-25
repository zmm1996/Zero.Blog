using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zero.Core.Entities;

namespace Zero.Infrastructure.DataBase
{
    public class Sys_UserActionLogMap : IEntityTypeConfiguration<Sys_UserActionLog>
    {
      

        public void Configure(EntityTypeBuilder<Sys_UserActionLog> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Controller)
                .HasMaxLength(128);

            builder.Property(t => t.Action)
                .HasMaxLength(128);
            builder.Property(t => t.ActionUserName)
               .HasMaxLength(128);
            builder.Property(t => t.ApiUrl)
                .HasMaxLength(128);

            builder.Property(t => t.IP)
                .HasMaxLength(50);

            builder.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            builder.ToTable("Sys_UserActionLog");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.Controller).HasColumnName("Controller");
            builder.Property(t => t.Action).HasColumnName("Action");
            builder.Property(t => t.ApiUrl).HasColumnName("ApiUrl");
            builder.Property(t => t.IP).HasColumnName("IP");
            builder.Property(t => t.ActionTime).HasColumnName("ActionTime");
            builder.Property(t => t.ActionUserId).HasColumnName("ActionUserId");
            builder.Property(t => t.ActionUserName).HasColumnName("ActionUserName");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.ActionType).HasColumnName("ActionType");
            builder.Property(t => t.ModelId).HasColumnName("ModelId");
            builder.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
