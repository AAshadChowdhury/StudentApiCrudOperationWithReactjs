using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCrudOperationWithReactjs.Migrations
{
    /// <inheritdoc />
    public partial class picture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentPictureUrl",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentPictureUrl",
                table: "Student");
        }
    }
}
