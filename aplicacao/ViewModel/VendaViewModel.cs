<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aplicacao.ViewModel
{
    public class VendaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório")]
        public int? ClienteId { get; set; }

        public string NomeCliente { get; set; }

        public DateTime DataDaVenda { get; set; }

        [Required(ErrorMessage = "O total da venda é obrigatório")]
        public decimal? TotalDaVenda { get; set; }

        [Required(ErrorMessage = "Os itens da venda é obrigatório")]
        public List<ItemVendaProdutoViewModel> ItensVenda { get; set; }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aplicacao.ViewModel
{
    public class VendaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório")]
        public int? ClienteId { get; set; }

        public string NomeCliente { get; set; }

        public DateTime DataDaVenda { get; set; }

        [Required(ErrorMessage = "O total da venda é obrigatório")]
        public decimal? TotalDaVenda { get; set; }

        [Required(ErrorMessage = "Os itens da venda é obrigatório")]
        public List<ItemVendaProdutoViewModel> ItensVenda { get; set; }
    }
}
>>>>>>> fad11f3e7c10c01b8efe32fd0d28703c9204a75f
