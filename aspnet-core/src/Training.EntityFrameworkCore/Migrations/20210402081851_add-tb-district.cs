using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Training.Migrations
{
    public partial class addtbdistrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpDistricts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpDistricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpDistricts_AbpProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "AbpProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpDistricts_ProvinceId",
                table: "AbpDistricts",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpDistricts");
        }
    }
}
