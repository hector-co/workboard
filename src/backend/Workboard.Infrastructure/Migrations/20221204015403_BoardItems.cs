using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Workboard.Infrastructure.Migrations
{
    public partial class BoardItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardItem",
                schema: "workboard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    BoardId = table.Column<int>(type: "integer", nullable: false),
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    ColumnId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardItem_Board_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "workboard",
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardItem_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "workboard",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardItem_Column_ColumnId",
                        column: x => x.ColumnId,
                        principalSchema: "workboard",
                        principalTable: "Column",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardItem_BoardId",
                schema: "workboard",
                table: "BoardItem",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardItem_CardId",
                schema: "workboard",
                table: "BoardItem",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardItem_ColumnId",
                schema: "workboard",
                table: "BoardItem",
                column: "ColumnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardItem",
                schema: "workboard");
        }
    }
}
