using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDificultyandRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("666b45b2-e595-43f4-9ddf-22bfa3aefbf7"), "Medium" },
                    { new Guid("6fa3457b-0b32-435a-9655-72fec73bec45"), "Hard" },
                    { new Guid("a5846353-4058-4d25-8009-5ce53bc477a2"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("69a8231e-22f4-4f72-99b6-e2b7bdc430e7"), "AKL", "AUCKLAND", "https://www.shutterstock.com/image-photo/beautiful-mountains-landscape-pictures-arang-kel-kashmir-2499596223" },
                    { new Guid("c60012f0-1dfe-4f2f-9af5-d8d7c5d499d6"), "KSM", "Kashmir", "https://www.shutterstock.com/image-photo/stunning-view-kashmir's-aru-valley-sunset-where-2473158391" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("666b45b2-e595-43f4-9ddf-22bfa3aefbf7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("6fa3457b-0b32-435a-9655-72fec73bec45"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("a5846353-4058-4d25-8009-5ce53bc477a2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "id",
                keyValue: new Guid("69a8231e-22f4-4f72-99b6-e2b7bdc430e7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "id",
                keyValue: new Guid("c60012f0-1dfe-4f2f-9af5-d8d7c5d499d6"));
        }
    }
}
