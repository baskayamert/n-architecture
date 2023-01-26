using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class EntityPropertyUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Hospitals_HospitalId1",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_HospitalId1",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HospitalId1",
                table: "Patients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HospitalId1",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_HospitalId1",
                table: "Patients",
                column: "HospitalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Hospitals_HospitalId1",
                table: "Patients",
                column: "HospitalId1",
                principalTable: "Hospitals",
                principalColumn: "Id");
        }
    }
}
