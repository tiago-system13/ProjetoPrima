<<<<<<< HEAD
﻿using Autofac;
using dominio.Conextos;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using infraEstrutura;
using dominio.Interfaces;
using dominio.Servicos;

namespace aplicacao
{
   public class AplicacaoModulo: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new ControleEstoqueContext("Base"))
             .As<DbContext>()
             .InstancePerLifetimeScope();           

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AplicacaoModulo)))
               .Where(x =>
                           x.Name
                            .StartsWith("Servico"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IServicoClienteRepositorio)))
               .Where(x =>
                           x.Name
                            .StartsWith("Servico"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
=======
﻿using Autofac;
using dominio.Conextos;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using infraEstrutura;
using dominio.Interfaces;
using dominio.Servicos;

namespace aplicacao
{
   public class AplicacaoModulo: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new ControleEstoqueContext("Base"))
             .As<DbContext>()
             .InstancePerLifetimeScope();           

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AplicacaoModulo)))
               .Where(x =>
                           x.Name
                            .StartsWith("Servico"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IServicoClienteRepositorio)))
               .Where(x =>
                           x.Name
                            .StartsWith("Servico"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
>>>>>>> fad11f3e7c10c01b8efe32fd0d28703c9204a75f
