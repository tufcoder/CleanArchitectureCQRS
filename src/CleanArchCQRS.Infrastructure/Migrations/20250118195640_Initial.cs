using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchCQRS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Genres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Mangas",
                columns: new[] { "Id", "DeletedAt", "Genres", "IsActive", "Price", "Publisher", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, null, "[0]", true, 9.99m, "Shueisha", new DateTime(1997, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "One Piece" },
                    { 2, null, "[0,4]", false, 7.99m, "Shueisha", new DateTime(2000, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Naruto" },
                    { 3, null, "[0,5,6]", true, 5.99m, "Kodansha", new DateTime(1990, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hajime no Ippo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mangas");
        }
    }
}
