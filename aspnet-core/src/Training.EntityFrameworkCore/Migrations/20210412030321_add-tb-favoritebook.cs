using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Training.Migrations
{
    public partial class addtbfavoritebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abp.FavoriteBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    BookLibraryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp.FavoriteBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp.FavoriteBooks_Abp.BookLibraries_BookLibraryId",
                        column: x => x.BookLibraryId,
                        principalTable: "Abp.BookLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abp.FavoriteBooks_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abp.FavoriteBooks_BookLibraryId",
                table: "Abp.FavoriteBooks",
                column: "BookLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp.FavoriteBooks_UserId",
                table: "Abp.FavoriteBooks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abp.FavoriteBooks");
        }
    }
}
