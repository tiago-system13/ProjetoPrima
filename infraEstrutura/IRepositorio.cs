using System;
using System.Linq;
using System.Linq.Expressions;

namespace infraEstrutura
{
    public interface IRepositorio<T, TKey> where T : class, IEntidade<TKey>
    {
        IQueryable<T> TodosIncluindo(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> Todos { get; }

        IQueryable<T> Listar();

        T PorId(object key);

        IQueryable<T> ListarPor(Expression<Func<T, bool>> predicate);

        ListaPaginavel<T> Listar(int indiceDaPagina, int tamanhoDaPagina, Expression<Func<T, TKey>> chaveSeletora, TipoDeOrdenacao orderBy);

        ListaPaginavel<T> Listar(int indiceDaPagina, int tamanhoDaPagina, Expression<Func<T, TKey>> chaveSeletora,
            Expression<Func<T, bool>> predicado, TipoDeOrdenacao orderBy, params Expression<Func<T, object>>[] incluindoPropriedades);

        void Inserir(T entidade);

        void Excluir(T entidade);

        void Excluir(TKey chaveDaEntidade);

        void Editar(T entidade);

        void Salvar();
    }
}
