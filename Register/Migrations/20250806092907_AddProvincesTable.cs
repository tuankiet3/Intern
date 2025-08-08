using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Register.Migrations
{
    /// <inheritdoc />
    public partial class AddProvincesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighSchool_Province_ProvinceId",
                table: "HighSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Area_AreaId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Aspiration_AspirationId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_HighSchool_HighSchoolId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Major_MajorId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Province_ProvinceId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Province",
                table: "Province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Major",
                table: "Major");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HighSchool",
                table: "HighSchool");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aspiration",
                table: "Aspiration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Area",
                table: "Area");

            migrationBuilder.RenameTable(
                name: "Province",
                newName: "Provinces");

            migrationBuilder.RenameTable(
                name: "Major",
                newName: "Majors");

            migrationBuilder.RenameTable(
                name: "HighSchool",
                newName: "HighSchools");

            migrationBuilder.RenameTable(
                name: "Aspiration",
                newName: "Aspirations");

            migrationBuilder.RenameTable(
                name: "Area",
                newName: "Areas");

            migrationBuilder.RenameIndex(
                name: "IX_HighSchool_ProvinceId",
                table: "HighSchools",
                newName: "IX_HighSchools_ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Majors",
                table: "Majors",
                column: "MajorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HighSchools",
                table: "HighSchools",
                column: "HighSchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aspirations",
                table: "Aspirations",
                column: "AspirationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighSchools_Provinces_ProvinceId",
                table: "HighSchools",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Areas_AreaId",
                table: "Students",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Aspirations_AspirationId",
                table: "Students",
                column: "AspirationId",
                principalTable: "Aspirations",
                principalColumn: "AspirationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_HighSchools_HighSchoolId",
                table: "Students",
                column: "HighSchoolId",
                principalTable: "HighSchools",
                principalColumn: "HighSchoolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Majors_MajorId",
                table: "Students",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "MajorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Provinces_ProvinceId",
                table: "Students",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighSchools_Provinces_ProvinceId",
                table: "HighSchools");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Areas_AreaId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Aspirations_AspirationId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_HighSchools_HighSchoolId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Majors_MajorId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Provinces_ProvinceId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Majors",
                table: "Majors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HighSchools",
                table: "HighSchools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aspirations",
                table: "Aspirations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "Province");

            migrationBuilder.RenameTable(
                name: "Majors",
                newName: "Major");

            migrationBuilder.RenameTable(
                name: "HighSchools",
                newName: "HighSchool");

            migrationBuilder.RenameTable(
                name: "Aspirations",
                newName: "Aspiration");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "Area");

            migrationBuilder.RenameIndex(
                name: "IX_HighSchools_ProvinceId",
                table: "HighSchool",
                newName: "IX_HighSchool_ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Province",
                table: "Province",
                column: "ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Major",
                table: "Major",
                column: "MajorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HighSchool",
                table: "HighSchool",
                column: "HighSchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aspiration",
                table: "Aspiration",
                column: "AspirationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Area",
                table: "Area",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighSchool_Province_ProvinceId",
                table: "HighSchool",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Area_AreaId",
                table: "Students",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Aspiration_AspirationId",
                table: "Students",
                column: "AspirationId",
                principalTable: "Aspiration",
                principalColumn: "AspirationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_HighSchool_HighSchoolId",
                table: "Students",
                column: "HighSchoolId",
                principalTable: "HighSchool",
                principalColumn: "HighSchoolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Major_MajorId",
                table: "Students",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "MajorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Province_ProvinceId",
                table: "Students",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
