<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;

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
=======
﻿using System.ComponentModel.DataAnnotations;

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
>>>>>>> fad11f3e7c10c01b8efe32fd0d28703c9204a75f
