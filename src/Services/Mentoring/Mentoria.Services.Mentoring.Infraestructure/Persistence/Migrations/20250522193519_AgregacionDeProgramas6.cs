using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregacionDeProgramas6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MentorAssignment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMentor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MentorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentorAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MentorAssignment_User_MentorId",
                        column: x => x.MentorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MentorAssignment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCareer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaximumNumberOfParticipants = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Program_Career_IdCareer",
                        column: x => x.IdCareer,
                        principalTable: "Career",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramActivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProgram = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramActivity_Program_IdProgram",
                        column: x => x.IdProgram,
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramActivity_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgramUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProgram = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramUser_Program_IdProgram",
                        column: x => x.IdProgram,
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramUser_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgramUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgramActivitySolution",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProgramActivity = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFileSolution = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Calification = table.Column<double>(type: "float", nullable: false),
                    ProgramActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramActivitySolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramActivitySolution_ProgramActivity_IdProgramActivity",
                        column: x => x.IdProgramActivity,
                        principalTable: "ProgramActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramActivitySolution_ProgramActivity_ProgramActivityId",
                        column: x => x.ProgramActivityId,
                        principalTable: "ProgramActivity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgramActivitySolution_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MentorAssignment_MentorId",
                table: "MentorAssignment",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_MentorAssignment_UserId",
                table: "MentorAssignment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_IdCareer",
                table: "Program",
                column: "IdCareer");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramActivity_IdProgram",
                table: "ProgramActivity",
                column: "IdProgram");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramActivity_ProgramId",
                table: "ProgramActivity",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramActivitySolution_IdProgramActivity",
                table: "ProgramActivitySolution",
                column: "IdProgramActivity");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramActivitySolution_ProgramActivityId",
                table: "ProgramActivitySolution",
                column: "ProgramActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramActivitySolution_UserId",
                table: "ProgramActivitySolution",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUser_IdProgram",
                table: "ProgramUser",
                column: "IdProgram");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUser_ProgramId",
                table: "ProgramUser",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUser_UserId",
                table: "ProgramUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MentorAssignment");

            migrationBuilder.DropTable(
                name: "ProgramActivitySolution");

            migrationBuilder.DropTable(
                name: "ProgramUser");

            migrationBuilder.DropTable(
                name: "ProgramActivity");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropColumn(
                name: "State",
                table: "User");
        }
    }
}
