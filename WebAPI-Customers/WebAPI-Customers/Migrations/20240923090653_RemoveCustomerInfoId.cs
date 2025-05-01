using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_Customers.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCustomerInfoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerInfos",
                table: "CustomerInfos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInfos_CustomerId",
                table: "CustomerInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CustomerInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerInfos",
                table: "CustomerInfos",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerInfos",
                table: "CustomerInfos");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "CustomerInfos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerInfos",
                table: "CustomerInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfos_CustomerId",
                table: "CustomerInfos",
                column: "CustomerId",
                unique: true);
        }
    }
}
