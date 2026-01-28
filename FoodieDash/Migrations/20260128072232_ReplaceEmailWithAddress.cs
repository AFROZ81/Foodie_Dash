using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodieDash.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceEmailWithAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "OrderHeaders");
        }
    }
}
