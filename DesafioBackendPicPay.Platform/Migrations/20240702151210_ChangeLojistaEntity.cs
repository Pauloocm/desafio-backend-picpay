using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioBackendPicPay.Platform.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLojistaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Lojistas",
                newName: "Cnpj");

            migrationBuilder.RenameIndex(
                name: "IX_Lojistas_Cpf",
                table: "Lojistas",
                newName: "IX_Lojistas_Cnpj");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "Lojistas",
                newName: "Cpf");

            migrationBuilder.RenameIndex(
                name: "IX_Lojistas_Cnpj",
                table: "Lojistas",
                newName: "IX_Lojistas_Cpf");
        }
    }
}
