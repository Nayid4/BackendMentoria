using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDeActividades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramActivitySolution_User_UserId",
                table: "ProgramActivitySolution");

            migrationBuilder.DropIndex(
                name: "IX_ProgramActivitySolution_UserId",
                table: "ProgramActivitySolution");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProgramActivitySolution");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramActivitySolution_IdUser",
                table: "ProgramActivitySolution",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramActivitySolution_User_IdUser",
                table: "ProgramActivitySolution",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramActivitySolution_User_IdUser",
                table: "ProgramActivitySolution");

            migrationBuilder.DropIndex(
                name: "IX_ProgramActivitySolution_IdUser",
                table: "ProgramActivitySolution");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ProgramActivitySolution",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramActivitySolution_UserId",
                table: "ProgramActivitySolution",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramActivitySolution_User_UserId",
                table: "ProgramActivitySolution",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
