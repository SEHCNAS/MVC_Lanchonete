using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Models;

namespace MVC.Repositories.Interfaces
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _Context;

        public LancheRepository(AppDbContext context) 
        {
            _Context = context;
        }

        /// <summary>
        /// Propriedade que retorna todos os lanches disponíveis no banco de dados.
        /// </summary>
        public IEnumerable<Lanche> Lanches => _Context.Lanches.Include(c => c.Categoria);

        /// <summary>
        /// Propriedade que retorna os lanches preferidos.
        /// </summary>
        public IEnumerable<Lanche> LanchesPreferidos => _Context.Lanches.Where(l => l.IsLanchePreferido).Include(c => c.Categoria);

        /// <summary>
        /// função que retorna um lanche específico com base no seu ID.
        /// </summary>
        public Lanche GetLanchesByID(int id)
        {
            return _Context.Lanches.FirstOrDefault(l => l.LancheId == id);
        }
    }
}
