using AutoMapper;
using DotNetWebApi;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Tests
{
    public class AutomapperTestHelper
    {
        public static IMapper Mapper = new Mapper(CreateConfiguration());
        private static MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(IWebApiMarker).Assembly);
                cfg.CreateMissingTypeMaps = true;
            });

            return config;
        }
    }
}
