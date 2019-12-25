using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Infrastructure.Migrations
{
    public partial class update_log : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActionUserName",
                table: "Sys_UserActionLog",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ActionUserName",
                table: "Sys_UserActionLog",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
