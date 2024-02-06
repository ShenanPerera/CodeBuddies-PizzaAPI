using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBuddies_PizzaAPI.Migrations
{
    /// <inheritdoc />
    public partial class add_totalField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "totalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "Orders");
        }
    }
}
