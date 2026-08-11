using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.FlowDesk.Migrations
{
    /// <inheritdoc />
    public partial class Added_UseCaseLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UseCaseLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UseCaseName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UseCaseData = table.Column<string>(type: "TEXT", nullable: false),
                    IsSuccessfull = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseCaseLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLogs_CreatedAt",
                table: "UseCaseLogs",
                column: "CreatedAt")
                .Annotation("SqlServer:Include", new[] { "Username", "UseCaseName", "IsSuccessfull" });

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLogs_CreatedAt_Username_UseCaseName",
                table: "UseCaseLogs",
                columns: new[] { "CreatedAt", "Username", "UseCaseName" })
                .Annotation("SqlServer:Include", new[] { "IsSuccessfull" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UseCaseLogs");
        }
    }
}
