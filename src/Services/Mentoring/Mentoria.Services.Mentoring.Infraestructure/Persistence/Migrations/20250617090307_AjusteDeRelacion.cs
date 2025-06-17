using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDeRelacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUser_Program_ProgramId",
                table: "ProgramUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUser_User_UserId",
                table: "ProgramUser");

            migrationBuilder.DropIndex(
                name: "IX_ProgramUser_ProgramId",
                table: "ProgramUser");

            migrationBuilder.DropIndex(
                name: "IX_ProgramUser_UserId",
                table: "ProgramUser");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "ProgramUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProgramUser");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUser_IdUser",
                table: "ProgramUser",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUser_User_IdUser",
                table: "ProgramUser",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUser_User_IdUser",
                table: "ProgramUser");

            migrationBuilder.DropIndex(
                name: "IX_ProgramUser_IdUser",
                table: "ProgramUser");

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId",
                table: "ProgramUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ProgramUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUser_ProgramId",
                table: "ProgramUser",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUser_UserId",
                table: "ProgramUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUser_Program_ProgramId",
                table: "ProgramUser",
                column: "ProgramId",
                principalTable: "Program",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUser_User_UserId",
                table: "ProgramUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
