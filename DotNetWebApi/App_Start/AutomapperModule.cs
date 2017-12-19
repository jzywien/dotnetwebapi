using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetWebApi
{
    public class AutomapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            Bind<IMapper>().ToMethod(ctx =>
                new Mapper(mapperConfiguration, type => ctx.Kernel.GetType()));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var thisAssembly = typeof(AutomapperModule).Assembly;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(thisAssembly);

                // Add type maps for anything which is not defined, 
                // where mapping is used.
                cfg.CreateMissingTypeMaps = true;
            });

            return config;
        }
    }
}