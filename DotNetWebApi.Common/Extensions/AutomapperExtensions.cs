using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DotNetWebApi.Common.Extensions
{
    public static class AutomapperExtensions
    {
        public static IMappingExpression<S, D> MapMember<S, D, TTo, TMember>(
            this IMappingExpression<S, D> map, 
            Expression<Func<D, TTo>> to, 
            Expression<Func<S, TMember>> from, 
            Action<IMemberConfigurationExpression<S, D, TTo>> opts = null, 
            bool skipEnsureMapping = false
        )
        {
            map.ForMember(to, o =>
            {
                o.MapFrom(from);
                if (opts != null)
                    opts(o);
            });

            return map;
        }

    }
}