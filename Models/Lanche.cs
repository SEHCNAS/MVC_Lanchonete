using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key] // Define a chave primária
        public int LancheId { get; set; }
        [Required(ErrorMessage = "O nome do lanche é obrigatório")]
        [Display(Name = "Nome do Lanche")]
        [StringLength(80, MinimumLength =10, ErrorMessage = "O tamanho maximo do campo é 80 caracteres!")]
        public string LancheNome { get; set; }

        [Required(ErrorMessage = "A descrição longa do lanche deve ser informada")]
        [Display(Name = "Descrição Curta")]
        [MinLength(20, ErrorMessage = "Descriçao deve ter no minimo {1} caracteres ")]
        [MaxLength(200, ErrorMessage = "Descriçao deve ter no maximo {1} caracteres ")]
        public string LancheDescricao { get; set; }

        [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
        [Display(Name = "Descrição Curta")]
        [MinLength(20, ErrorMessage = "Descriçao deve ter no minimo {1} caracteres ")]
        [MaxLength(200, ErrorMessage = "Descriçao deve ter no maximo {1} caracteres ")]
        public string LancheDescricaoCurta { get; set; }

        [Display(Name = "Caminho da imagem")]
        [StringLength(200, ErrorMessage = "O tamanho maximo do campo é 200 caracteres!")]
        public string LancheImagemUrl { get; set; }

        [Display(Name = "Caminho da miniatura")]
        [StringLength(200, ErrorMessage = "O tamanho maximo do campo é 200 caracteres!")]
        public string LancheImagemThumb { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1, 9999.99, ErrorMessage = "O preço deve ser entre {1} e {2}")]
        public decimal LanchePreco { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Em estoque?")]
        public bool EmEstoque { get; set; }
        
        // CategoriaId é a chave estrangeira
        public int CategoriaId { get; set; }

        // Propriedade de navegação
        //Definendo a relação 1:N entre Categoria e Lanche
        public virtual Categoria Categoria { get; set; }
    }
}
