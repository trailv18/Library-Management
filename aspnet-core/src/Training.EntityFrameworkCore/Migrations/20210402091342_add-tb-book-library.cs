using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Training.Migrations
{
    public partial class addtbbooklibrary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abp.BookLibraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    LibraryId = table.Column<Guid>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp.BookLibraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp.BookLibraries_Abp.Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Abp.Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abp.BookLibraries_Abp.Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Abp.Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abp.BookLibraries_BookId",
                table: "Abp.BookLibraries",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp.BookLibraries_LibraryId",
                table: "Abp.BookLibraries",
                column: "LibraryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abp.BookLibraries");
        }
    }
}
