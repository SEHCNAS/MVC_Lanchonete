using MVC.Models;

namespace MVC.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        /// <summary>
        /// Propriedade que retorna todos os lanches disponíveis no banco de dados.
        /// </summary>
        IEnumerable<Lanche> Lanches { get; }

        /// <summary>
        /// Propriedade que retorna os lanches preferidos.
        /// </summary>
        IEnumerable<Lanche> LanchesPreferidos { get; }

        /// <summary>
        /// Método qu retorna um lanche específico com base no seu ID.
        /// </summary>
        Lanche GetLanchesByID(int id);


    }
}
