using MVC.Models;

namespace MVC.ViewModels
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }
        public string CategoriaAtual { get; set; }

        // Construtor para inicializar a lista de lanches
        public LancheListViewModel(IEnumerable<Lanche> lanches, string categoriaAtual)
        {
            Lanches = lanches;
            CategoriaAtual = categoriaAtual;
        }

        // Construtor padrão
        public LancheListViewModel()
        {
            Lanches = new List<Lanche>();
            CategoriaAtual = string.Empty;
        }
    }
}
