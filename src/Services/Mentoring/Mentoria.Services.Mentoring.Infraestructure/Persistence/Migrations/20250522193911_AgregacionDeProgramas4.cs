using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregacionDeProgramas4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MentorAssignment_User_MentorId",
                table: "MentorAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_MentorAssignment_User_UserId",
                table: "MentorAssignment");

            migrationBuilder.DropIndex(
                name: "IX_MentorAssignment_MentorId",
                table: "MentorAssignment");

            migrationBuilder.DropIndex(
                name: "IX_MentorAssignment_UserId",
                table: "MentorAssignment");

            migrationBuilder.DropColumn(
                name: "MentorId",
                table: "MentorAssignment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MentorAssignment");

            migrationBuilder.CreateIndex(
                name: "IX_MentorAssignment_IdMentor",
                table: "MentorAssignment",
                column: "IdMentor");

            migrationBuilder.CreateIndex(
                name: "IX_MentorAssignment_IdUser",
                table: "MentorAssignment",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_MentorAssignment_User_IdMentor",
                table: "MentorAssignment",
                column: "IdMentor",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MentorAssignment_User_IdUser",
                table: "MentorAssignment",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MentorAssignment_User_IdMentor",
                table: "MentorAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_MentorAssignment_User_IdUser",
                table: "MentorAssignment");

            migrationBuilder.DropIndex(
                name: "IX_MentorAssignment_IdMentor",
                table: "MentorAssignment");

            migrationBuilder.DropIndex(
                name: "IX_MentorAssignment_IdUser",
                table: "MentorAssignment");

            migrationBuilder.AddColumn<Guid>(
                name: "MentorId",
                table: "MentorAssignment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "MentorAssignment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MentorAssignment_MentorId",
                table: "MentorAssignment",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_MentorAssignment_UserId",
                table: "MentorAssignment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MentorAssignment_User_MentorId",
                table: "MentorAssignment",
                column: "MentorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MentorAssignment_User_UserId",
                table: "MentorAssignment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
