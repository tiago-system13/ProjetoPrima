using System;
using System.ComponentModel.DataAnnotations;

namespace aplicacao.ViewModel
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="O CPF é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        public DateTime? DataDeNascimento { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }
    }
}
