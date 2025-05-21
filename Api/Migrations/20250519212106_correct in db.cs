using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleaningSaboms.Migrations
{
    /// <inheritdoc />
    public partial class correctindb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerAddressId",
                table: "Customers",
                column: "CustomerAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAddresses_CustomerAddressId",
                table: "Customers",
                column: "CustomerAddressId",
                principalTable: "CustomerAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAddresses_CustomerAddressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerAddressId",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "CustomerAddresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
