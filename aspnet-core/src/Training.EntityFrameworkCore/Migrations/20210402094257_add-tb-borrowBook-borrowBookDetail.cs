using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Training.Migrations
{
    public partial class addtbborrowBookborrowBookDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abp.BorrowBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateBorrow = table.Column<DateTime>(nullable: false),
                    DateRepay = table.Column<DateTime>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp.BorrowBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp.BorrowBooks_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abp.BorrowBookDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BorrowBookId = table.Column<Guid>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    PriceBorrow = table.Column<int>(nullable: false),
                    Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp.BorrowBookDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp.BorrowBookDetails_Abp.Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Abp.Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abp.BorrowBookDetails_Abp.BorrowBooks_BorrowBookId",
                        column: x => x.BorrowBookId,
                        principalTable: "Abp.BorrowBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abp.BorrowBookDetails_BookId",
                table: "Abp.BorrowBookDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp.BorrowBookDetails_BorrowBookId",
                table: "Abp.BorrowBookDetails",
                column: "BorrowBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp.BorrowBooks_UserId",
                table: "Abp.BorrowBooks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abp.BorrowBookDetails");

            migrationBuilder.DropTable(
                name: "Abp.BorrowBooks");
        }
    }
}
