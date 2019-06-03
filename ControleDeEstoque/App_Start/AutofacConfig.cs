using Autofac;
using Autofac.Integration.Mvc;
using aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using dominio;

namespace ControleDeEstoque.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
            builder.Register(c => HttpContext.Current.User).InstancePerRequest();
            builder.Register(c => HttpContext.Current.Session).InstancePerRequest();
            /// <summary>
            /// Registre aqui seu módulo
            /// </summary>
            builder.RegisterModule<DominioModulo>();
            builder.RegisterModule<AplicacaoModulo>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}