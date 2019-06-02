using dominio.Mappins;
using dominio.Models;
using System.Data.Entity;

namespace dominio.Conextos
{
    public class ControleEstoqueContext : DbContext
    {
        public ControleEstoqueContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<ControleEstoqueContext>(null);
            Configuration.LazyLoadingEnabled = true;
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public ControleEstoqueContext() : base("Base")
        {
            Database.SetInitializer<ControleEstoqueContext>(null);
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<ItemVendaProduto> ItemVendaProdutos { get; set; }

        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClienteMapping());
            modelBuilder.Configurations.Add(new ProdutoMapping());
            modelBuilder.Configurations.Add(new ItemVendaProdutoMapping());
            modelBuilder.Configurations.Add(new VendaMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}


