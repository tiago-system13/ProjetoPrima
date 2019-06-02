

namespace infraEstrutura
{
    /// <summary>
    /// Subclasses devem ser feitas para facilitar a pesquisa paginada
    /// </summary>
    public abstract class PesquisaPorPagina
    {
        /// <summary>
        /// Indice de uma página referida
        /// </summary>
        public int IndiceDePagina { get; set; }

        /// <summary>
        /// Quantidade de Registros por Página
        /// </summary>
        public int RegistrosPorPagina { get; set; }

        /// <summary>
        /// Tipo da Ordenação
        /// </summary>
        public TipoDeOrdenacao Ordenacao { get; set; }

        /// <summary>
        /// Coluna para seleção
        /// </summary>
        public string Coluna { get; set; }

        /// <summary>
        /// Construtor padrão. Por Padrão o Índice de Página é 1, a Quantidade de Registros por Página é 10 e a Ordenação é por Ordem Crescente
        /// </summary>
        public PesquisaPorPagina()
        {
            IndiceDePagina = 1;
            RegistrosPorPagina = 10;
            Ordenacao = TipoDeOrdenacao.ASC;
        }
    }
}
