using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationAndOrderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "order_agreement_pdf_file_name",
                table: "work_orders",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "order_pdf_file_name",
                table: "work_orders",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_agreement_pdf_file_name",
                table: "work_orders");

            migrationBuilder.DropColumn(
                name: "order_pdf_file_name",
                table: "work_orders");
        }
    }
}
