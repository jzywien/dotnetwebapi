using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWebApi.Common
{
    public interface ICustomMapper<TTarget, TSource>
    {
        void ExtendSourceToTargetMapping(IMappingExpression<TSource, TTarget> map);
        void ExtendTargetToSourceMapping(IMappingExpression<TTarget, TSource> map);
    }

    public abstract class CustomMapper<TTarget, TSource> : Profile, ICustomMapper<TTarget, TSource>
    {
        protected CustomMapper()
        {
            var targetToSourceMap = CreateMap<TTarget, TSource>();
            ExtendTargetToSourceMapping(targetToSourceMap);

            var sourceToTargetMap = CreateMap<TSource, TTarget>();
            ExtendSourceToTargetMapping(sourceToTargetMap);
        }

        public abstract void ExtendSourceToTargetMapping(IMappingExpression<TSource, TTarget> map);

        public abstract void ExtendTargetToSourceMapping(IMappingExpression<TTarget, TSource> map);
    }
}
