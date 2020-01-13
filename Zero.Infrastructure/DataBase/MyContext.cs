using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Entities;
using Zero.Core.Entities.Sys;
using Zero.Util.Helpers;

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

            #region 种子数据
            var parrentid = NumberNo.SequentialGuid();
            //种子数据
            modelBuilder.Entity<Sys_User>().HasData(new Sys_User
            {
                Id = NumberNo.SequentialGuid(),
                LoginName = "administrator",
                DisplayName = "超级管理员",
                Password = "zmm123",
                UserType = 0
            });
            modelBuilder.Entity<Sys_Menu>().HasData(new Sys_Menu()
            {
                Id = parrentid,
                Name = "用户权限管理",
                Url = "1",
                Icon = "el-icon-setting",
                ParentId = Guid.Empty,
                ParentName = "顶级菜单",
                Component = "1"
            },
            new Sys_Menu()
            {
                Id = NumberNo.SequentialGuid(),
                Name = "角色管理",
                Url = "/role",
                Icon = "md-menu",
                ParentId = parrentid,
                ParentName = "用户权限管理",
                Component = "Role"
            },
            new Sys_Menu()
            {
                Id = NumberNo.SequentialGuid(),
                Name = "菜单管理",
                Url = "/menu",
                Icon = "md-menu",
                ParentId = parrentid,
                ParentName = "用户权限管理",
                Component = "Menu"
            },
            new Sys_Menu()
            {
                Id = NumberNo.SequentialGuid(),
                Name = "角色权限分配",
                Url = "/rolepermission",
                Icon = "md-menu",
                ParentId = parrentid,
                ParentName = "用户权限管理",
                Component = "RolePermission"
            },
            new Sys_Menu()
            {
                Id = NumberNo.SequentialGuid(),
                Name = "权限管理",
                Url = "/permission",
                Icon = "md-menu",
                ParentId = parrentid,
                ParentName = "用户权限管理",
                Component = "Permission"
            },
            new Sys_Menu()
            {
                Id = NumberNo.SequentialGuid(),
                Name = "用户管理",
                Url = "/user",
                Icon = "md-menu",
                ParentId = parrentid,
                ParentName = "用户权限管理",
                Component = "User"
            },
            new Sys_Menu()
            {
                Id = NumberNo.SequentialGuid(),
                Name = "操作记录",
                Url = "/actionlog",
                Icon = "md-menu",
                ParentId = Guid.Empty,
                ParentName = "顶级菜单",
                Component = "ActionLog"
            }

            ); 
            #endregion

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
