using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalk.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandReagions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Regions",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2b779f69-6f1b-4dd0-9736-192b98943f01"), "Medium" },
                    { new Guid("6e8a8d57-46d7-4941-9ffd-e97ae65ba0c5"), "Easy" },
                    { new Guid("e0543ddb-eb25-45ca-ac0a-38890b54ed34"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("24999999-d924-48ba-9d12-780797723865"), "NTL", "Northland", null },
                    { new Guid("77882740-f5a9-492c-b7b5-da94f52ecbfe"), "AKL", "Ackland", "https://www.freepik.com/free-photos-vectors/auckland" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("2b779f69-6f1b-4dd0-9736-192b98943f01"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6e8a8d57-46d7-4941-9ffd-e97ae65ba0c5"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("e0543ddb-eb25-45ca-ac0a-38890b54ed34"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("24999999-d924-48ba-9d12-780797723865"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("77882740-f5a9-492c-b7b5-da94f52ecbfe"));

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Regions",
                newName: "id");
        }
    }
}
