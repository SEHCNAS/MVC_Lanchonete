using MVC.Context;
using MVC.Models;
using MVC.Repositories.Interfaces;

namespace MVC.Repositories
{
    /// <summary>
    /// Repositório de acesso às categorias, implementando a interface ICategoriaRepository.
    /// </summary>
    public class CategoriaRepository : ICategoriaRepository
    {
        /// <summary>
        /// Contexto do banco de dados usado para acessar os dados.
        /// </summary>
        private readonly AppDbContext _Context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        public CategoriaRepository(AppDbContext context)
        {
            _Context = context;
        }

        /// <summary>
        /// Propriedade que retorna todas as categorias disponíveis no banco de dados.
        /// </summary>
        public IEnumerable<Categoria> Categorias => _Context.Categorias;
    }

}
