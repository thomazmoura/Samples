using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExemplosDeSincronismo.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AcrescentarStage1ERowVersionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PessoaStage1Id",
                table: "Pessoas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Pessoas",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "PessoaStage1Id",
                table: "Compras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PessoasStage1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataDeNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    OriginalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoasStage1", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_PessoaStage1Id",
                table: "Pessoas",
                column: "PessoaStage1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_PessoaStage1Id",
                table: "Compras",
                column: "PessoaStage1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_PessoasStage1_PessoaStage1Id",
                table: "Compras",
                column: "PessoaStage1Id",
                principalTable: "PessoasStage1",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_PessoasStage1_PessoaStage1Id",
                table: "Pessoas",
                column: "PessoaStage1Id",
                principalTable: "PessoasStage1",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_PessoasStage1_PessoaStage1Id",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_PessoasStage1_PessoaStage1Id",
                table: "Pessoas");

            migrationBuilder.DropTable(
                name: "PessoasStage1");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_PessoaStage1Id",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Compras_PessoaStage1Id",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "PessoaStage1Id",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "PessoaStage1Id",
                table: "Compras");
        }
    }
}
