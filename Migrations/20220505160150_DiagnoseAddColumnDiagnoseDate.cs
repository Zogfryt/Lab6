using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_First.Migrations
{
    public partial class DiagnoseAddColumnDiagnoseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DiagnoseDate",
                table: "Diagnoses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2000, 1, 1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiagnoseDate",
                table: "Diagnoses");
        }
    }
}
