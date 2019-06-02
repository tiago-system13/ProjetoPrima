using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControleDeEstoque.Startup))]
namespace ControleDeEstoque
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
        }
    }
}
