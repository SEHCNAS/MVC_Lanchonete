using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Context
{
    public class AppDbContext : DbContext
    {
        // Construtor da classe
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        // Método para configurar o modelo
        // Propriedades de DbSet para as entidades
        public DbSet<Lanche> Lanches { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
    }
}
