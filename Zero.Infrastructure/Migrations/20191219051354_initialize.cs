using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Infrastructure.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true),
                    Alias = table.Column<string>(maxLength: 255, nullable: true),
                    Icon = table.Column<string>(maxLength: 128, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    ParentName = table.Column<string>(maxLength: 50, nullable: true),
                    Level = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Sort = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<int>(nullable: true),
                    IsDefaultRouter = table.Column<int>(nullable: true),
                    Component = table.Column<string>(maxLength: 255, nullable: true),
                    HideInMenu = table.Column<int>(nullable: true),
                    NotCache = table.Column<int>(nullable: true),
                    BeforeCloseFun = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: true),
                    CreatedByUserName = table.Column<string>(maxLength: 128, nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: true),
                    ModifiedByUserName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    ActionCode = table.Column<string>(maxLength: 80, nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    IsDelete = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: true),
                    CreatedByUserName = table.Column<string>(maxLength: 128, nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: true),
                    ModifiedByUserName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<int>(nullable: true),
                    IsSuperAdministrator = table.Column<bool>(nullable: true),
                    IsBuiltin = table.Column<bool>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: true),
                    CreatedByUserName = table.Column<string>(maxLength: 128, nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: true),
                    ModifiedByUserName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_RolePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: true),
                    PermissionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RolePermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LoginName = table.Column<string>(maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 128, nullable: true),
                    Avatar = table.Column<string>(maxLength: 255, nullable: true),
                    UserType = table.Column<int>(nullable: true),
                    IsLocked = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: true),
                    CreatedByUserName = table.Column<string>(maxLength: 128, nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: true),
                    ModifiedByUserName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserActionLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Controller = table.Column<string>(maxLength: 128, nullable: true),
                    Action = table.Column<string>(maxLength: 128, nullable: true),
                    ApiUrl = table.Column<string>(maxLength: 128, nullable: true),
                    IP = table.Column<string>(maxLength: 50, nullable: true),
                    ActionTime = table.Column<DateTime>(nullable: true),
                    ActionUserId = table.Column<Guid>(nullable: true),
                    ActionUserName = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    ActionType = table.Column<int>(nullable: true),
                    ModelId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserActionLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserRole", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Menu");

            migrationBuilder.DropTable(
                name: "Sys_Permission");

            migrationBuilder.DropTable(
                name: "Sys_Role");

            migrationBuilder.DropTable(
                name: "Sys_RolePermission");

            migrationBuilder.DropTable(
                name: "Sys_User");

            migrationBuilder.DropTable(
                name: "Sys_UserActionLog");

            migrationBuilder.DropTable(
                name: "Sys_UserRole");
        }
    }
}
