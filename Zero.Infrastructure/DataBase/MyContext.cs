using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Entities;
using Zero.Core.Entities.Sys;

namespace Zero.Infrastructure.DataBase
{
   public class MyContext:DbContext
    {
        public MyContext(
            DbContextOptions<MyContext> options
            ):base(options)
        {

        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //添加对应的视图映射关系
            modelBuilder.ApplyConfiguration(new Sys_MenuMap());
            modelBuilder.ApplyConfiguration(new Sys_PermissionMap());
            modelBuilder.ApplyConfiguration(new Sys_RoleMap());
            modelBuilder.ApplyConfiguration(new Sys_RolePermissionMap());
            modelBuilder.ApplyConfiguration(new Sys_UserMap());
            modelBuilder.ApplyConfiguration(new Sys_UserActionLogMap());
            modelBuilder.ApplyConfiguration(new Sys_UserRoleMap());
        }

        public DbSet<Sys_Menu> Sys_Menu { get; set; }
        public DbSet<Sys_Permission> Sys_Permission { get; set; }
        public DbSet<Sys_Role> Sys_Role { get; set; }
        public DbSet<Sys_RolePermission> Sys_RolePermission { get; set; }
        public DbSet<Sys_User> Sys_User { get; set; }
        public DbSet<Sys_UserActionLog> Sys_UserActionLog { get; set; }
        public DbSet<Sys_UserRole> Sys_UserRole { get; set; }


        //角色权限（前台展示）
        public DbQuery<Sys_PermissionWithAssignProperty> Sys_PermissionWithAssignProperty { get; set; }
    }
}
