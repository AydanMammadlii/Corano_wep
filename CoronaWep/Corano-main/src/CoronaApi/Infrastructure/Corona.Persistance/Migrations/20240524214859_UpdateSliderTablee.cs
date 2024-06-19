using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corona.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSliderTablee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Sliders",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Sliders",
                newName: "ImagePath");
        }
    }
}
