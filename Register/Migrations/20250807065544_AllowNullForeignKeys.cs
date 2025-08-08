using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Register.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Aspirations_AspirationId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "AspirationId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Aspirations_AspirationId",
                table: "Students",
                column: "AspirationId",
                principalTable: "Aspirations",
                principalColumn: "AspirationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Aspirations_AspirationId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "AspirationId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Aspirations_AspirationId",
                table: "Students",
                column: "AspirationId",
                principalTable: "Aspirations",
                principalColumn: "AspirationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
