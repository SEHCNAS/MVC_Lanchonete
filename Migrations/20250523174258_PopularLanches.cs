using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class PopularLanches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Populando a tabela de lanches com dados iniciais
            migrationBuilder.InsertData(
            table: "Lanches",
            columns: new[] { "LancheNome", "LancheDescricao", "LancheDescricaoCurta", "LancheImagemUrl", "LancheImagemThumb", "LanchePreco", "IsLanchePreferido", "EmEstoque", "CategoriaId" },
            values: new object[,]
            {
                { "Cheeseburger Clássico", "Um delicioso cheeseburger com carne grelhada e queijo derretido.", "Cheeseburger com carne e queijo.", "/images/cheeseburger.jpg", "/images/thumbs/cheeseburger.jpg", 18.90m, true, true, 1 },
                { "X-Bacon Supremo", "Hambúrguer suculento com bacon crocante, queijo e molho especial.", "Hambúrguer com bacon e queijo.", "/images/xbacon.jpg", "/images/thumbs/xbacon.jpg", 21.50m, true, true, 1 },
                { "Vegetariano Light", "Sanduíche leve com hambúrguer de grão-de-bico e vegetais frescos.", "Lanche vegetariano com sabor.", "/images/vegetariano.jpg", "/images/thumbs/vegetariano.jpg", 19.90m, false, true, 2 },
                { "Frango Crocante", "Pão macio com filé de frango empanado e maionese caseira.", "Lanche de frango empanado.", "/images/frango.jpg", "/images/thumbs/frango.jpg", 20.00m, false, true, 1 },
                { "Mini Burger Kids", "Mini hambúrguer ideal para crianças, com queijo e ketchup.", "Lanche infantil saboroso.", "/images/kids.jpg", "/images/thumbs/kids.jpg", 14.50m, false, true, 1 },
                { "Double Smash", "Dois hambúrgueres smash com cheddar e molho especial.", "Lanche duplo com cheddar.", "/images/doublesmash.jpg", "/images/thumbs/doublesmash.jpg", 23.00m, true, true, 1 },
                { "Fish Burger", "Lanche com filé de peixe empanado, alface e molho tártaro.", "Sanduíche de peixe.", "/images/fish.jpg", "/images/thumbs/fish.jpg", 18.00m, false, true, 2 },
                { "Cheddar Melt", "Hambúrguer com cheddar cremoso e cebola caramelizada.", "Cheddar com cebola.", "/images/cheddarmelt.jpg", "/images/thumbs/cheddarmelt.jpg", 22.50m, true, true, 1 },
                { "Veggie Grill", "Sanduíche vegetariano com legumes grelhados e molho verde.", "Legumes grelhados e pão integral.", "/images/veggiegrill.jpg", "/images/thumbs/veggiegrill.jpg", 20.90m, false, true, 2 },
                { "Triplo Bacon Max", "Lanche com três camadas de bacon, queijo e hambúrguer.", "Bacon em dobro, sabor em triplo.", "/images/triplobacon.jpg", "/images/thumbs/triplobacon.jpg", 25.00m, true, true, 1 }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("DELETE FROM Lanches");
            
        }
    }
}
