using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExemplosDeSincronismo.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AcrescentarCompraEItemDeCompraComoJsonMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDaCompra");

            migrationBuilder.AddColumn<string>(
                name: "ItensDaCompra",
                table: "Compras",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItensDaCompra",
                table: "Compras");

            migrationBuilder.CreateTable(
                name: "ItemDaCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDaCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDaCompra_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemDaCompra_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemDaCompra_CompraId",
                table: "ItemDaCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDaCompra_ProdutoId",
                table: "ItemDaCompra",
                column: "ProdutoId");
        }
    }
}
