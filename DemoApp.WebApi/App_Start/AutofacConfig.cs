﻿using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using DemoApp.Business;
using DemoApp.Data.Context;
using DemoApp.Data.Mapping;
using DemoApp.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DemoApp.WebApi
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var bldr = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //RegisterServices(bldr);
            bldr.RegisterWebApiFilterProvider(config);
            bldr.RegisterWebApiModelBinderProvider();
            var container = bldr.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        //private static void RegisterServices(ContainerBuilder bldr)
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile(new EmployeeMappingProfile());
        //    });

        //    bldr.RegisterInstance(config.CreateMapper())
        //                .As<IMapper>()
        //                .SingleInstance();



        //    bldr.RegisterType<EmployeeContext>()
        //.InstancePerRequest();

        //    bldr.RegisterType<EmployeeRepository>()
        //      .As<IEmployeeRepository>()
        //      .InstancePerRequest();
        //}
    }
}
