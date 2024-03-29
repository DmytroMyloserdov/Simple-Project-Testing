﻿using Autofac;
using Autofac.Integration.WebApi;
using SqlServer.Context;
using SqlServer.Interfaces;
using SqlServer.Repositories;
using System.Reflection;
using System.Web.Http;

namespace SimpleProjectTesting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
            );

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var simpleContext = new SimpleContext();
            builder.RegisterInstance(simpleContext).As<SimpleContext>().SingleInstance();

            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            }
        }
    }
}
