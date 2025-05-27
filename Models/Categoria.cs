using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key] // Define a chave primária
        public int CategoriaId { get; set; }

        [StringLength(100, ErrorMessage = "O tamanho maximo do campo é 100 caracteres!")]
        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [Display(Name = "Nome da Categoria")]
        public string CategoriaNome { get; set; }

        [StringLength(200, ErrorMessage = "O tamanho maximo do campo é 200 caracteres!")]
        [Required(ErrorMessage = "A descrição é obrigatório")]
        [Display(Name = "Descrição")]
        public string CategoriaDescricao { get; set; }

        //Definindo a relação 1:N entre Categoria e Lanche
        public List<Lanche> Lanches { get; set; }
}
}
