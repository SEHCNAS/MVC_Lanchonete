using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Populando a tabela de categorias com dados iniciais
            // CategoriaId é auto-incremento, então não precisamos especificar
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, CategoriaDescricao) VALUES ('Normal', 'Lanche Normal')");
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, CategoriaDescricao) VALUES ('Lanche Natural', 'Lanche Natural')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categorias");
        }
    }
}
