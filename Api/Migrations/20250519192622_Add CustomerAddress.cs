using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleaningSaboms.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAddresses_CustomerAddressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerAddressId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "CustomerAddresses",
                newName: "CustomerPostalCode");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "CustomerAddresses",
                newName: "CustomerCity");

            migrationBuilder.RenameColumn(
                name: "AddressLine",
                table: "CustomerAddresses",
                newName: "CustomerAddressLine");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.RenameColumn(
                name: "CustomerPostalCode",
                table: "CustomerAddresses",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "CustomerCity",
                table: "CustomerAddresses",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "CustomerAddressLine",
                table: "CustomerAddresses",
                newName: "AddressLine");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
