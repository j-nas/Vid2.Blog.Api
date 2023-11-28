using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vid2.Blog.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    completion_result = table.Column<string>(type: "text", nullable: false),
                    youtube_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_results", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_results_youtube_id",
                table: "results",
                column: "youtube_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "results");
        }
    }
}
