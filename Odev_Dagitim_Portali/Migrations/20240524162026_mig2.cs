using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Odev_Dagitim_Portali.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Homework_submissions");

            migrationBuilder.CreateTable(
                name: "HomeworkSubmissions",
                columns: table => new
                {
                    Submission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Homework_id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkSubmissions", x => x.Submission_id);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmissions_AspNetUsers_User_id",
                        column: x => x.User_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmissions_Homeworks_Homework_id",
                        column: x => x.Homework_id,
                        principalTable: "Homeworks",
                        principalColumn: "Homework_id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissions_Homework_id",
                table: "HomeworkSubmissions",
                column: "Homework_id");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissions_User_id",
                table: "HomeworkSubmissions",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeworkSubmissions");

            migrationBuilder.CreateTable(
                name: "Homework_submissions",
                columns: table => new
                {
                    Submission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Homework_id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    File_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homework_submissions", x => x.Submission_id);
                    table.ForeignKey(
                        name: "FK_Homework_submissions_AspNetUsers_User_id",
                        column: x => x.User_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homework_submissions_Homeworks_Homework_id",
                        column: x => x.Homework_id,
                        principalTable: "Homeworks",
                        principalColumn: "Homework_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Homework_submissions_Homework_id",
                table: "Homework_submissions",
                column: "Homework_id");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_submissions_User_id",
                table: "Homework_submissions",
                column: "User_id");
        }
    }
}
