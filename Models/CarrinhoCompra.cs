using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Migrations;

namespace MVC.Models
{
    /// <summary>
    /// Representa um carrinho de compras, gerenciando itens e o total.
    /// </summary>
    public class CarrinhoCompra
    {

        // Campo somente leitura para o contexto do banco de dados, injetado via construtor.
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Construtor da classe CarrinhoCompra.
        /// </summary>
        /// <param name="appDbContext">O contexto do banco de dados da aplicação.</param>
        public CarrinhoCompra(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Obtém ou define o ID único do carrinho de compras.
        /// </summary>
        public string CarrinhoCompraId { get; set; }

        /// <summary>
        /// Obtém ou define a lista de itens dentro do carrinho de compras.
        /// </summary>
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        /// <summary>
        /// Obtém uma instância do CarrinhoCompra, seja da sessão existente ou criando um novo.
        /// </summary>
        /// <param name="services">Provedor de serviços da aplicação para acessar a sessão e o contexto do DB.</param>
        /// <returns>Uma instância de CarrinhoCompra.</returns>
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // Define e obtém a sessão HTTP atual. A sessão é usada para armazenar o ID do carrinho.
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // Obtém o contexto do banco de dados (AppDbContext) através do provedor de serviços.
            var context = services.GetService<AppDbContext>();

            // Tenta obter o ID do carrinho de compras da sessão.
            // Se não existir (primeira vez do usuário ou sessão expirada), cria um novo ID GUID.
            string CarrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Atribui o novo ID gerado à sessão para futuras requisições.
            session.SetString("CarrinhoId", CarrinhoId);

            // Retorna uma nova instância de CarrinhoCompra com o contexto do banco de dados e o ID atribuído.
            // A lista de itens é inicializada vazia, será preenchida ao carregar do DB.
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = CarrinhoId,
                CarrinhoCompraItens = new List<CarrinhoCompraItem>() // Inicializa a lista de itens.
            };

        }

        /// <summary>
        /// Adiciona um lanche ao carrinho de compras ou incrementa sua quantidade se já existir.
        /// </summary>
        /// <param name="lanche">O objeto Lanche a ser adicionado.</param>
        public void AdicionarAoCarrinho(Lanche lanche)
        {
            // Procura por um item de carrinho existente que corresponda ao lanche e ao ID do carrinho atual.
            var CarrinhoCompraItem = _appDbContext.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId
                );

            // Se o item não for encontrado no carrinho (é um lanche novo para o carrinho).
            if (CarrinhoCompraItem == null)
            {
                // Cria um novo item de carrinho.
                CarrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId, // Associa ao ID do carrinho atual.
                    Lanche = lanche,                    // Associa o lanche.
                    Quantidade = 1                      // Define a quantidade inicial como 1.
                };
                // Adiciona o novo item ao contexto do banco de dados.
                _appDbContext.CarrinhoCompraItens.Add(CarrinhoCompraItem);
            }
            else
            {
                // Se o item já existe, apenas incrementa a quantidade.
                CarrinhoCompraItem.Quantidade++;
            }

            // Salva as mudanças no banco de dados.
            _appDbContext.SaveChanges();
        }

        /// <summary>
        /// Remove um item do carrinho de compras ou decrementa sua quantidade.
        /// </summary>
        /// <param name="lanche">O objeto Lanche a ser removido.</param>
        /// <returns>A quantidade restante do item no carrinho após a remoção, ou 0 se o item for completamente removido.</returns>
        public int RemoverItemCarrinho(Lanche lanche)
        {
            // Procura pelo item de carrinho existente que corresponde ao lanche e ao ID do carrinho atual.
            var CarrinhoCompraItem = _appDbContext.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId
                );

            var quantidadeLocal = 0; // Variável para armazenar a quantidade restante do item.

            // Se o item for encontrado no carrinho.
            if (CarrinhoCompraItem != null)
            {
                // Se a quantidade do item for maior que 1, apenas decrementa.
                if (CarrinhoCompraItem.Quantidade > 1)
                {
                    CarrinhoCompraItem.Quantidade--;
                    quantidadeLocal = CarrinhoCompraItem.Quantidade; // Atualiza a quantidade local.
                }
                else
                {
                    // Se a quantidade for 1 (ou menor), remove completamente o item do carrinho.
                    _appDbContext.CarrinhoCompraItens.Remove(CarrinhoCompraItem);
                }
            }

            // Salva as mudanças no banco de dados.
            _appDbContext.SaveChanges();
            return quantidadeLocal; // Retorna a quantidade atualizada.
        }

        /// <summary>
        /// Obtém todos os itens do carrinho de compras para a instância atual do carrinho.
        /// Inclui os detalhes do Lanche para cada item.
        /// </summary>
        /// <returns>Uma lista de CarrinhoCompraItem.</returns>
        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            // Se a lista de itens do carrinho ainda não foi carregada (nula), então a carrega do banco de dados.
            // Caso contrário, retorna a lista já carregada (otimização para evitar múltiplas chamadas ao DB).
            return CarrinhoCompraItens ?? (CarrinhoCompraItens = _appDbContext.CarrinhoCompraItens
                // Filtra os itens pelo ID do carrinho atual.
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                // Inclui a entidade Lanche relacionada a cada item do carrinho.
                .Include(s => s.Lanche)
                // Converte o resultado para uma lista.
                .ToList());
        }

        /// <summary>
        /// Limpa todos os itens do carrinho de compras para a instância atual.
        /// </summary>
        public void LimparCarrinho()
        {
            // Obtém todos os itens do carrinho associados ao ID do carrinho atual.
            var carrinhoCompraItens = _appDbContext.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

            // Remove todos os itens encontrados do contexto do banco de dados.
            _appDbContext.CarrinhoCompraItens.RemoveRange(carrinhoCompraItens);
            // Salva as mudanças no banco de dados.
            _appDbContext.SaveChanges();
        }

        /// <summary>
        /// Calcula o valor total do carrinho de compras para a instância atual.
        /// </summary>
        /// <returns>O valor decimal total dos itens no carrinho.</returns>
        public decimal GetCarrinhoCompraTotal()
        {
            // Calcula o total somando o preço de cada lanche multiplicado pela sua quantidade.
            // O filtro garante que apenas os itens do carrinho atual são considerados.
            var total = _appDbContext.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.LanchePreco * c.Quantidade).Sum();

            return total;
        }
    }
}