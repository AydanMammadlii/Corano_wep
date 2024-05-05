using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corona.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class BasketProductAddColumnCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BasketProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "BasketProducts");
        }
    }
}
