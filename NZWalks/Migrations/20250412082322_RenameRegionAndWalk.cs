using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameRegionAndWalk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_Regionsid",
                table: "Walks");

            migrationBuilder.DropIndex(
                name: "IX_Walks_Regionsid",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "Regionsid",
                table: "Walks");

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "id",
                keyValue: new Guid("69a8231e-22f4-4f72-99b6-e2b7bdc430e7"),
                column: "Name",
                value: "Auckland");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks");

            migrationBuilder.DropIndex(
                name: "IX_Walks_RegionId",
                table: "Walks");

            migrationBuilder.AddColumn<Guid>(
                name: "Regionsid",
                table: "Walks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "id",
                keyValue: new Guid("69a8231e-22f4-4f72-99b6-e2b7bdc430e7"),
                column: "Name",
                value: "AUCKLAND");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_Regionsid",
                table: "Walks",
                column: "Regionsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_Regionsid",
                table: "Walks",
                column: "Regionsid",
                principalTable: "Regions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
