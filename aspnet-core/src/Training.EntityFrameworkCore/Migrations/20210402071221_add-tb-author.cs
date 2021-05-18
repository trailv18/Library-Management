using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Training.Migrations
{
    public partial class addtbauthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpAuthors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    YearOfBirth = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpAuthors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpAuthors");
        }
    }
}
