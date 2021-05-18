using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Training.Migrations
{
    public partial class addtblibrary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abp.Libraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<Guid>(nullable: false),
                    DistrictId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abp.Libraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abp.Libraries_Abp.Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Abp.Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abp.Libraries_Abp.Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Abp.Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abp.Libraries_DistrictId",
                table: "Abp.Libraries",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Abp.Libraries_ProvinceId",
                table: "Abp.Libraries",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abp.Libraries");
        }
    }
}
