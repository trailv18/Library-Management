using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Training.Migrations
{
    public partial class addtbbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false),
                    PriceBorrow = table.Column<int>(nullable: false),
                    PublisherId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    YearPublic = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpBooks_AbpAuthors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AbpAuthors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpBooks_AbpCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AbpCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpBooks_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpBooks_AuthorId",
                table: "AbpBooks",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpBooks_CategoryId",
                table: "AbpBooks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpBooks_PublisherId",
                table: "AbpBooks",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpBooks");
        }
    }
}
