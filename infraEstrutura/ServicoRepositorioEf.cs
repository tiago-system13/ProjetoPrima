using infraEstrutura;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace infraEstrutura
{
    public class ServicoRepositorioEf<T, TKey> : IRepositorio<T, TKey>
        where T : class, IEntidade<TKey>
    {
        protected readonly DbContext _entitiesContext;

        public ServicoRepositorioEf(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }

            _entitiesContext = entitiesContext;
        }

        public virtual IQueryable<T> Listar()
        {
            return _entitiesContext.Set<T>();
        }

        public virtual IQueryable<T> Todos { get { return Listar(); } }

        public virtual IQueryable<T> TodosIncluindo(
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entitiesContext.Set<T>();
            return includeProperties
                .Aggregate(query, (q, y) => q.Include(y));
        }

        public T PorId(object key)
        {
            return Listar().FirstOrDefault(x => key == (object)x.Id);
        }

        public virtual IQueryable<T> ListarPor(
            Expression<Func<T, bool>> predicate)
        {
            return _entitiesContext
                        .Set<T>()
                            .Where(predicate);
        }

        public virtual ListaPaginavel<T> Listar(
            int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, TipoDeOrdenacao orderBy)
        {
            return Listar(pageIndex, pageSize, keySelector, orderBy);
        }

        public virtual ListaPaginavel<T> Listar(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate, TipoDeOrdenacao orderBy,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = TodosIncluindo(includeProperties);

            query = (predicate == null) ? query : query.Where(predicate);

            return query.ParaListaPaginavel(pageIndex, pageSize, orderBy, keySelector);
        }

        private void SetEntryStateOf(T entity, EntityState to)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = to;
        }

        public virtual void Inserir(T entity)
        {
            _entitiesContext.Set<T>().Add(entity);
            SetEntryStateOf(entity, to: EntityState.Added);
        }

        public virtual void Editar(T entity)
        {
            SetEntryStateOf(entity, to: EntityState.Modified);
        }

        public virtual void Excluir(T entity)
        {
            SetEntryStateOf(entity, to: EntityState.Deleted);
        }


        public virtual void Excluir(TKey entityId)
        {
            var entity = _entitiesContext.Set<T>().Find(entityId);
            SetEntryStateOf(entity, to: EntityState.Deleted);
        }

        public virtual void Salvar()
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                _entitiesContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


           
        }

    }
}
