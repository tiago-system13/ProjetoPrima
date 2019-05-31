using System.ComponentModel.DataAnnotations;

namespace aplicacao.ViewModel
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string NomeDoProduto { get; set; }

        [Required(ErrorMessage = "O quantidade é obrigatória")]
        public int? QuantidadeDoProduto { get; set; }

        [Required(ErrorMessage = "O valor unitário é obrigatório")]
        public decimal? ValorUnitario { get; set; }
    }
}
