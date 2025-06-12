using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregacionDeProgramas7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicInformation_Career_CareerId",
                table: "AcademicInformation");

            migrationBuilder.DropIndex(
                name: "IX_AcademicInformation_CareerId",
                table: "AcademicInformation");

            migrationBuilder.DropColumn(
                name: "CareerId",
                table: "AcademicInformation");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicInformation_IdCareer",
                table: "AcademicInformation",
                column: "IdCareer");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicInformation_Career_IdCareer",
                table: "AcademicInformation",
                column: "IdCareer",
                principalTable: "Career",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicInformation_Career_IdCareer",
                table: "AcademicInformation");

            migrationBuilder.DropIndex(
                name: "IX_AcademicInformation_IdCareer",
                table: "AcademicInformation");

            migrationBuilder.AddColumn<Guid>(
                name: "CareerId",
                table: "AcademicInformation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicInformation_CareerId",
                table: "AcademicInformation",
                column: "CareerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicInformation_Career_CareerId",
                table: "AcademicInformation",
                column: "CareerId",
                principalTable: "Career",
                principalColumn: "Id");
        }
    }
}
