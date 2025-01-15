using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExemplosDeIEnumerableEIQueryable.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AcrescentarApelidoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apelido",
                table: "Pessoas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apelido",
                table: "Pessoas");
        }
    }
}
