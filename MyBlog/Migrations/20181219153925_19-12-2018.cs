using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.Migrations
{
    public partial class _19122018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BlogPost",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedOn",
                table: "BlogPost",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "PostedOn",
                table: "BlogPost");
        }
    }
}
