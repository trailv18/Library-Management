using Microsoft.EntityFrameworkCore.Migrations;

namespace Training.Migrations
{
    public partial class edittb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpBooks_AbpAuthors_AuthorId",
                table: "AbpBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpBooks_AbpCategories_CategoryId",
                table: "AbpBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpBooks_Publishers_PublisherId",
                table: "AbpBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpDistricts_AbpProvinces_ProvinceId",
                table: "AbpDistricts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProvinces",
                table: "AbpProvinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpDistricts",
                table: "AbpDistricts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpCategories",
                table: "AbpCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpBooks",
                table: "AbpBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpAuthors",
                table: "AbpAuthors");

            migrationBuilder.RenameTable(
                name: "Publishers",
                newName: "Abp.Publishers");

            migrationBuilder.RenameTable(
                name: "AbpProvinces",
                newName: "Abp.Provinces");

            migrationBuilder.RenameTable(
                name: "AbpDistricts",
                newName: "Abp.Districts");

            migrationBuilder.RenameTable(
                name: "AbpCategories",
                newName: "Abp.Categories");

            migrationBuilder.RenameTable(
                name: "AbpBooks",
                newName: "Abp.Books");

            migrationBuilder.RenameTable(
                name: "AbpAuthors",
                newName: "Abp.Authors");

            migrationBuilder.RenameIndex(
                name: "IX_AbpDistricts_ProvinceId",
                table: "Abp.Districts",
                newName: "IX_Abp.Districts_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpBooks_PublisherId",
                table: "Abp.Books",
                newName: "IX_Abp.Books_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpBooks_CategoryId",
                table: "Abp.Books",
                newName: "IX_Abp.Books_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpBooks_AuthorId",
                table: "Abp.Books",
                newName: "IX_Abp.Books_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abp.Publishers",
                table: "Abp.Publishers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abp.Provinces",
                table: "Abp.Provinces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abp.Districts",
                table: "Abp.Districts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abp.Categories",
                table: "Abp.Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abp.Books",
                table: "Abp.Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abp.Authors",
                table: "Abp.Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Abp.Books_Abp.Authors_AuthorId",
                table: "Abp.Books",
                column: "AuthorId",
                principalTable: "Abp.Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Abp.Books_Abp.Categories_CategoryId",
                table: "Abp.Books",
                column: "CategoryId",
                principalTable: "Abp.Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Abp.Books_Abp.Publishers_PublisherId",
                table: "Abp.Books",
                column: "PublisherId",
                principalTable: "Abp.Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Abp.Districts_Abp.Provinces_ProvinceId",
                table: "Abp.Districts",
                column: "ProvinceId",
                principalTable: "Abp.Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abp.Books_Abp.Authors_AuthorId",
                table: "Abp.Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Abp.Books_Abp.Categories_CategoryId",
                table: "Abp.Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Abp.Books_Abp.Publishers_PublisherId",
                table: "Abp.Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Abp.Districts_Abp.Provinces_ProvinceId",
                table: "Abp.Districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abp.Publishers",
                table: "Abp.Publishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abp.Provinces",
                table: "Abp.Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abp.Districts",
                table: "Abp.Districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abp.Categories",
                table: "Abp.Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abp.Books",
                table: "Abp.Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abp.Authors",
                table: "Abp.Authors");

            migrationBuilder.RenameTable(
                name: "Abp.Publishers",
                newName: "Publishers");

            migrationBuilder.RenameTable(
                name: "Abp.Provinces",
                newName: "AbpProvinces");

            migrationBuilder.RenameTable(
                name: "Abp.Districts",
                newName: "AbpDistricts");

            migrationBuilder.RenameTable(
                name: "Abp.Categories",
                newName: "AbpCategories");

            migrationBuilder.RenameTable(
                name: "Abp.Books",
                newName: "AbpBooks");

            migrationBuilder.RenameTable(
                name: "Abp.Authors",
                newName: "AbpAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_Abp.Districts_ProvinceId",
                table: "AbpDistricts",
                newName: "IX_AbpDistricts_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Abp.Books_PublisherId",
                table: "AbpBooks",
                newName: "IX_AbpBooks_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Abp.Books_CategoryId",
                table: "AbpBooks",
                newName: "IX_AbpBooks_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Abp.Books_AuthorId",
                table: "AbpBooks",
                newName: "IX_AbpBooks_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProvinces",
                table: "AbpProvinces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpDistricts",
                table: "AbpDistricts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpCategories",
                table: "AbpCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpBooks",
                table: "AbpBooks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpAuthors",
                table: "AbpAuthors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpBooks_AbpAuthors_AuthorId",
                table: "AbpBooks",
                column: "AuthorId",
                principalTable: "AbpAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpBooks_AbpCategories_CategoryId",
                table: "AbpBooks",
                column: "CategoryId",
                principalTable: "AbpCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpBooks_Publishers_PublisherId",
                table: "AbpBooks",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpDistricts_AbpProvinces_ProvinceId",
                table: "AbpDistricts",
                column: "ProvinceId",
                principalTable: "AbpProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
